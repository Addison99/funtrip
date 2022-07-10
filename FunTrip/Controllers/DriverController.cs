using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using AutoMapper;
using FunTrip.DTOs;
using System.Collections.Generic;
using System.Linq;
using System;
using DataAccess.Paging;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using System.IO;
using Azure.Storage.Blobs.Models;

namespace FunTrip.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private IDriverRepository driverRepository;
        private IAccountRepository accountRepository;
        private IGroupRepository groupRepository;
        private readonly IMapper mapper;
        public DriverController(IDriverRepository _driverRepository, IMapper _mapper
            ,IAccountRepository accountRepository,IGroupRepository groupRepository)
        {
            this.driverRepository = _driverRepository;
            this.mapper = _mapper;
            this.accountRepository = accountRepository;
            this.groupRepository = groupRepository;
        }               
        [HttpGet("{id}")]
        public DriverDTO Get(int id)
        {
            Driver driver = driverRepository.Get(id);
            DriverDTO driverDTO;
            if (driver == null) driverDTO = null;
            else driverDTO = mapper.Map<DriverDTO>(driver);
            return driverDTO;
        }
        [HttpDelete("{id}")]
        public string delete(int id)
        {
            Driver driver = driverRepository.Get(id);
            driver.Account = accountRepository.Get((int)driver.AccountId);
            driver.Account.Status = "Inactive";
            accountRepository.Update(driver.Account);
            return "Delete Success";
        }
        [HttpGet("")]
        public IEnumerable<DriverDTO> Search(string? DriverName,int? groupID, int? CategoryID,bool all, float? rate, int pageNumber, int pageSize)
        {
            if (pageNumber == 0) pageNumber = 1;
            if (pageSize == 0) pageSize = 10;
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            if (all == true)
            {
                IEnumerable<Driver> drivers1 = driverRepository.GetList(x => x.Id > 0);
                return new PagedList<Driver>(drivers1.AsQueryable(), pageNumber, pageSize)
                    .List.Select(x => mapper.Map<DriverDTO>(x));
            }
            Dictionary<int,Driver> drivers = new Dictionary<int,Driver>();
            if (DriverName != null)
            {
                IEnumerable<Driver> driverss = driverRepository.GetList(x => x.FullName.Contains(DriverName));
                foreach (Driver driver in driverss)
                    if (!drivers.ContainsKey(driver.Id)) drivers.Add(driver.Id, driver);
            }
            if (rate != null)
            {
                IEnumerable<Driver> driverss = driverRepository.GetList(x=> x.Bookings.Average(x => x.Rate) >=rate);
                foreach (Driver driver in driverss)
                    if (!drivers.ContainsKey(driver.Id)) drivers.Add(driver.Id, driver);
            }
            if (groupID != null)
            {
                IEnumerable<Driver> driverss = driverRepository.GetList(x => x.GroupId == groupID);
                foreach(Driver driver in driverss)
                    if (!drivers.ContainsKey(driver.Id)) drivers.Add(driver.Id, driver);
            }
            PagedList<Driver> pagedList = new PagedList<Driver>(drivers.Values.AsQueryable(),pageNumber,pageSize);
            IEnumerable<DriverDTO> driverDTOs = pagedList.List.Select
                (
                    x => mapper.Map<DriverDTO>(x)
                    );
            return driverDTOs;

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DriverDTO driverDTO, [FromForm] IFormFile file)
        {
                Driver driver = mapper.Map<Driver>(driverDTO);
                Account acc = new Account()
                {
                    Password = driver.Password,
                    Username = driver.Username,
                    Email = driverDTO.Email,
                    RoleId = 3,
                    Status = "Active"
                };

                var checkLoginResult = accountRepository.GetList((account) => (account.Email.Equals(driverDTO.Email) || account.Username.Equals(driverDTO.Username)));
                if (checkLoginResult.Count() > 0) return Unauthorized();

                accountRepository.Create(acc);
                int accID = accountRepository.GetMax();
                driver.AccountId = accID;
                if (file != null)
                {
                    await uploadFile(file);
                    driver.Img = "https:/merry.blob.core.windows.net/yume/" + file.FileName;
                }

                driverRepository.Create(driver);
            
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<string> Update(int id, [FromForm] DriverDTO driverdto, [FromForm] IFormFile file)
        {
            try
            {
                Driver driver = mapper.Map<Driver>(driverdto);
                Driver driver1 = driverRepository.Get(id);
                driver1.FullName = driver.FullName;
                driver1.Address = driver.Address;
                driver1.CreditCard = driver.CreditCard;
                Account acc = accountRepository.Get((int)driver1.AccountId);
                acc.Email = driverdto.Email;
                acc.Password = driverdto.Password;
                driver1.Account = acc;
                driver1.Group = groupRepository.Get((int) driverdto.GroupId);
                if (file != null)
                {
                    await deleteFile(driver.Img);
                    await uploadFile(file);
                    if (file != null)
                    {
                        driver1.Img = "https:/merry.blob.core.windows.net/yume/" + file.FileName;
                    }
                }
                
                driverRepository.Update(driver1);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Update Success";
        }
        private BlobContainerClient GetBlobContainerClient()
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=merry;AccountKey=AOHLpp9ABjn/pEwmw6skcyzHGoujukf2KFTAkWFBt8LpSZ19cTohCv/bLXhMrRBJqHqok47dVRRk+ASt1s4qRA==;EndpointSuffix=core.windows.net";
            string containerName = "yume";
            return new BlobContainerClient(connectionString, containerName);
        }
        private async Task deleteFile(string filename)
        {
            if (filename == null) return;
            filename = filename.Substring(filename.LastIndexOf("/") + 1);
            var container = GetBlobContainerClient();
            try
            {
                var blobClient = container.GetBlobClient(filename);
                await blobClient.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<String> uploadFile(IFormFile file)
        {
            var container = GetBlobContainerClient();
                var blobClient = container.GetBlobClient(file.FileName);
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    ms.Position = 0;
                    var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };
                    await blobClient.UploadAsync(ms, new BlobUploadOptions { HttpHeaders = blobHttpHeader });
                    ;
                }
                return "https:/merry.blob.core.windows.net/yume/" + file.FileName;
            }
        }
}
