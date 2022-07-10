using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.IRepository;
using DataAccess.Repository;
using BusinessObject.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FunTrip.DTOs;
using AutoMapper;
using System;
using System.Threading.Tasks;
using System.IO;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using DataAccess.Paging;

namespace FunTrip.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        IVehicleRepository vehicleRepository;
        IMapper mapper;
        public VehicleController(IMapper mapper, IVehicleRepository vehicleRepository)
        {
            this.mapper = mapper;
            this.vehicleRepository = vehicleRepository;
        }
        [HttpGet("{id}")]
        public VehicleDTO get(int id)
        {
            return mapper.Map<VehicleDTO>(vehicleRepository.Get(id));
        }
        [HttpDelete("{id}")]
        public void delete(int id)
        {
            Vehicle v = vehicleRepository.Get(id);
            v.Status = "Inactive";
            vehicleRepository.Update(v);
        }
        [HttpGet]
        public IEnumerable<VehicleDTO> search
            (string? name, int? categoryId, string? driverName, string? manufacturer
                ,bool all,int pageNumber,int pageSize) 
        {
            if (pageNumber ==0) pageNumber = 1;
            if (pageSize == 0) pageSize = 10;
            if (all == true)
            {
                PagedList<Vehicle> pageList1 = new PagedList<Vehicle>(
                vehicleRepository.GetList(x=> x.Status == "Active").AsQueryable() ,
                pageNumber, pageSize);
                return pageList1.List.Select(x => mapper.Map<VehicleDTO>(x));
            }
            Dictionary<int, Vehicle> dic = new Dictionary<int, Vehicle>();
            if (name != null)
            {
                var vehicles = vehicleRepository.GetList(x => x.VehicleName.Contains(name) && x.Status == "Active");
                foreach(var vehicle in vehicles)
                    if (!dic.ContainsKey(vehicle.Id)) dic.Add(vehicle.Id, vehicle);
            }
            if (categoryId != null)
            {
                var vehicles = vehicleRepository.GetList(x => x.CategoryId==categoryId && x.Status == "Active");
                foreach (var vehicle in vehicles)
                    if (!dic.ContainsKey(vehicle.Id)) dic.Add(vehicle.Id, vehicle);
            }
            if (driverName != null)
            {
                var vehicles = vehicleRepository.GetList(x => x.Driver.FullName.Contains(driverName) && x.Status == "Active");
                foreach (var vehicle in vehicles)
                    if (!dic.ContainsKey(vehicle.Id)) dic.Add(vehicle.Id, vehicle);
            }
            if (manufacturer != null)
            {
                var vehicles = vehicleRepository.GetList(x => x.Manufacturer == manufacturer && x.Status == "Active");
                foreach (var vehicle in vehicles)
                    if (!dic.ContainsKey(vehicle.Id)) dic.Add(vehicle.Id, vehicle);
            }

            PagedList<Vehicle> pageList = new PagedList<Vehicle>(
                dic.Values.AsQueryable() ,
                pageNumber, pageSize);
            return pageList.List.Select(x => mapper.Map<VehicleDTO>(x));
        }
        [HttpPost("")]
        public async Task<string> create([FromForm] VehicleDTO dto, [FromForm] IFormFile file)
        {
            Vehicle vehicle = mapper.Map<Vehicle>(dto);
            vehicle.Status = "Active";
            try
            {
                await uploadFile(file);
                vehicle.Img = "https:/merry.blob.core.windows.net/yume/" + file.FileName;
                vehicleRepository.Create(vehicle);
            }catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "OK";
        }
        [HttpPut("{id}")]
        public async Task<string> update(int id,[FromForm] VehicleDTO dto, [FromForm] IFormFile file)
        {
            Vehicle v = vehicleRepository.Get(id);
            v.VehicleName = dto.VehicleName;
            v.Manufacturer = dto.Manufacturer;
            v.CategoryId = (int)dto.CategoryId;
            v.DriverId = dto.DriverId;
            
            try
            {
                await deleteFile(v.Img);
                await uploadFile(file);
                v.Img = "https:/merry.blob.core.windows.net/yume/" + file.FileName;
                vehicleRepository.Update(v);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "OK";
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
            try
            {
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private BlobContainerClient GetBlobContainerClient()
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=merry;AccountKey=AOHLpp9ABjn/pEwmw6skcyzHGoujukf2KFTAkWFBt8LpSZ19cTohCv/bLXhMrRBJqHqok47dVRRk+ASt1s4qRA==;EndpointSuffix=core.windows.net";
            string containerName = "yume";
            return new BlobContainerClient(connectionString, containerName);
        }
    }
}