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

namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private IDriverRepository driverRepository;
        private IAccountRepository accountRepository;
        private readonly IMapper mapper;
        public DriverController(IDriverRepository _driverRepository, IMapper _mapper,IAccountRepository accountRepository)
        {
            this.driverRepository = _driverRepository;
            this.mapper = _mapper;
            this.accountRepository = accountRepository;
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
        public IEnumerable<DriverDTO> Search(string? DriverName,int? groupID, int? CategoryID, float? rate, int pageNumber, int pageSize)
        {
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            Dictionary<int,Driver> drivers = new Dictionary<int,Driver>();
            if (DriverName != null)
            {
                IEnumerable<Driver> driverss = driverRepository.GetList(x => x.FullName.Contains(DriverName));
                foreach (Driver driver in driverss)
                    if (!drivers.ContainsKey(driver.Id)) drivers.Add(driver.Id, driver);
            }
            if (rate != null)
            {
                IEnumerable<Driver> driverss = driverRepository.GetList(x=> x.Orders.Average(x => x.Rate) >=rate);
                foreach (Driver driver in driverss)
                    if (!drivers.ContainsKey(driver.Id)) drivers.Add(driver.Id, driver);
            }
            if (groupID != null)
            {
                IEnumerable<Driver> driverss = driverRepository.GetList(x => x.GroupId == groupID);
                foreach(Driver driver in driverss)
                    if (!drivers.ContainsKey(driver.Id)) drivers.Add(driver.Id, driver);
            }
            if (CategoryID!= null)
            {
                IEnumerable<Driver> driverss = driverRepository.GetList(x=> x.Vehicle.CategoryId == CategoryID).ToList();
                foreach(Driver driver in driverss)
                    if (!drivers.ContainsKey(driver.Id)) drivers.Add(driver.Id,driver);
            }
            PagedList<Driver> pagedList = new PagedList<Driver>(drivers.Values.AsQueryable(),pageNumber,pageSize);
            IEnumerable<DriverDTO> driverDTOs = pagedList.List.Select
                (
                    x => mapper.Map<DriverDTO>(x)
                    );
            return driverDTOs;

        }
        [HttpPost("")]
        public string Create([FromBody] DriverDTO driverDTO)
        {
            try
            {
                Driver driver = mapper.Map<Driver>(driverDTO);
                Account acc = new Account()
                {
                    Password = driver.Password,
                    Username = driver.Username,
                    Email = driver.Gmail,
                    RoleId = 3,
                    Status = "Active"
                };
                accountRepository.Create(acc);
                int accID = accountRepository.GetMax();
                driver.AccountId = accID;
                driverRepository.Create(driver);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
            return "Create Success";
        }
        [HttpPut("")]
        public string Update([FromBody] DriverDTO driverdto)
        {
            try
            {
                Driver driver = mapper.Map<Driver>(driverdto);
                Driver driver1 = driverRepository.Get(driver.Id);
                driver1.FullName = driver.FullName;
                driver1.Address = driver.Address;
                driver1.CreditCard = driver.CreditCard;
                Account acc = accountRepository.Get((int)driver1.AccountId);
                acc.Email = driverdto.Gmail;
                acc.Password = driverdto.Password;
                driver1.Account = acc;
                driverRepository.Update(driver1);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Update Success";
        }
    }
}
