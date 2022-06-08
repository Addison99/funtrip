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
namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        ICityRepository _cityRepository;
        IMapper _mapper;
        public CityController(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public CityDTO get(int id)
        {
            return _mapper.Map<CityDTO>(_cityRepository.Get(id));
        }
        [HttpDelete("{id}")]
        public string delete(int id)
        {
            City city = _cityRepository.Get(id);
            city.Status = "Inactive";
            _cityRepository.Update(city);
            return "OK";
        }
        [HttpGet("")]
        public IEnumerable<CityDTO> search(string? name)
        {
            return _cityRepository.GetList(x=> x.City1.Contains(name) && x.Status == "Active")
                .Select(x => _mapper.Map<CityDTO>(x));
        }
        [HttpPost("")]
        public string create([FromBody] CityDTO dto)
        {
            City city = _mapper.Map<City>(dto);
            city.Status = "Active";
            _cityRepository.Create(city);
            return "Ok";
        }
        [HttpPut("")]
        public string update([FromBody] CityDTO dto)
        {
            City city = _cityRepository.Get(dto.Id);
            city.City1 = dto.City1;
            _cityRepository.Update(city);
            return "OK";
        }
    }
}
