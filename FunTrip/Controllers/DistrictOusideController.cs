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
    public class DistrictOusideController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDistrictOutsideRepository _outsideRepository;
        public DistrictOusideController(IMapper mapper, IDistrictOutsideRepository repository)
        {
            _mapper = mapper;
            _outsideRepository = repository;
        }
        [HttpGet("{id}")]
        public DistrictOutsideDTO get(int id)
        {
            return _mapper.Map<DistrictOutsideDTO>(_outsideRepository.Get(id));
        }
        [HttpDelete("{id}")]
        public void delete(int id)
        {
            DistrictOutside district = _outsideRepository.Get(id);
            district.Status = "Inactive";
            _outsideRepository.Update(district);
        }
        [HttpGet("")]
        public IEnumerable<DistrictOutsideDTO> search(string? name, string? cityName)
        {
            Dictionary<int, DistrictOutside> dic = new Dictionary<int, DistrictOutside>();
            if (name != null)
            {
                var districts = _outsideRepository.GetList(x => x.District.Contains(name) && x.Status == "Active");
                foreach (var district in districts)
                    if (!dic.ContainsKey(district.Id)) dic.Add(district.Id, district);
            }
            if (cityName != null)
            {
                var districts = _outsideRepository.GetList(x => x.City.City1 == cityName && x.Status == "Active");
                foreach (var district in districts)
                    if (!dic.ContainsKey(district.Id)) dic.Add(district.Id, district);
            }
            return dic.Values.Select(x => _mapper.Map<DistrictOutsideDTO>(x));
        }
        [HttpPost("")]
        public string create([FromBody]DistrictOutsideDTO dto)
        {
            DistrictOutside district = _mapper.Map<DistrictOutside>(dto);
            district.Status = "Active";
            try
            {
                _outsideRepository.Create(district);
            }catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";
        }
        [HttpPut("")]
        public string update([FromBody] DistrictOutsideDTO dto)
        {
            DistrictOutside district = _outsideRepository.Get(dto.Id);
            district.District = dto.District;
            district.CityId = dto.CityId;
            try
            {
                _outsideRepository.Update(district);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "OK";
        }
    }
}
