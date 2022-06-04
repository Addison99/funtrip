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

namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private IDriverRepository driverRepository;
        private readonly IMapper mapper;
        public DriverController(IDriverRepository _driverRepository, IMapper _mapper)
        {
            this.driverRepository = _driverRepository;
            this.mapper = _mapper;
        }               
        [HttpGet("get")]
        public DriverDTO Get(int id)
        {
            Driver driver = driverRepository.Get(id);
            DriverDTO driverDTO;
            if (driver == null) driverDTO = null;
            else driverDTO = mapper.Map<DriverDTO>(driver);
            return driverDTO;
        }
        [HttpGet("search")]
        public IEnumerable<DriverDTO> Search(string? DriverName,int? groupID, int? CategoryID,float? rate)
        {
            Dictionary<int,Driver> drivers = new Dictionary<int,Driver>();
            if (DriverName != null)
            {
                IEnumerable<Driver> driverss = driverRepository.GetList(x => x.FullName.Contains(DriverName));
                foreach (Driver driver in driverss)
                    if (!drivers.ContainsKey(driver.Id)) drivers.Add(driver.Id, driver);
            }
            if (rate != null)
            {
                IEnumerable<Driver> driverss = driverRepository.GetList(x=> x.Orders.Sum(x => x.Rate)/ x.Orders.Count >=rate);
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
            var driverDTOs = drivers.Values.Select
                                            (
                                            x => mapper.Map<DriverDTO>(x)
                                            );
            return driverDTOs;

        }
        [HttpPost("Create")]
        public string Create(DriverDTO driverDTO)
        {
            try
            {
                Driver driver = mapper.Map<Driver>(driverDTO);
                driverRepository.Create(driver);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
            return "Create Success";
        }
        [HttpPost("Update")]
        public string Update(Driver driver)
        {
            try
            {
                driverRepository.Create(driver);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Create Success";
        }
    }
}
