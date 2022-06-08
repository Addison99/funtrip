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

namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("")]
        public IEnumerable<VehicleDTO> search(string? name, int? categoryId, string? driverName, string? manufacturer) 
        {
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
                var vehicles = vehicleRepository.GetList(x => x.Drivers.First().FullName.Contains(driverName) && x.Status == "Active");
                foreach (var vehicle in vehicles)
                    if (!dic.ContainsKey(vehicle.Id)) dic.Add(vehicle.Id, vehicle);
            }
            if (manufacturer != null)
            {
                var vehicles = vehicleRepository.GetList(x => x.Manufacturer == manufacturer && x.Status == "Active");
                foreach (var vehicle in vehicles)
                    if (!dic.ContainsKey(vehicle.Id)) dic.Add(vehicle.Id, vehicle);
            }
            return dic.Values.Select(x=> mapper.Map<VehicleDTO>(x)).ToList();
        }
        [HttpPost("")]
        public string create([FromBody] VehicleDTO dto)
        {
            Vehicle vehicle = mapper.Map<Vehicle>(dto);
            vehicle.Status = "Active";
            try
            {
                vehicleRepository.Create(vehicle);
            }catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "OK";
        }
        [HttpPut("")]
        public string update([FromBody] VehicleDTO dto)
        {
            Vehicle v = vehicleRepository.Get(dto.Id);
            v.VehicleName = dto.VehicleName;
            v.Manufacturer = dto.Manufacturer;
            v.CategoryId = dto.CategoryId;
            try
            {
                vehicleRepository.Update(v);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return "OK";
        }
    }
}
