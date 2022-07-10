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
using DataAccess.Paging;

namespace FunTrip.Controllers
{
    [Route("api/districts")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        IDistrictRepository districtRepository { get; set; }
        IMapper mapper { get; set; }
        public DistrictController(IDistrictRepository districtRepository, IMapper mapper)
        {
            this.districtRepository = districtRepository;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public DistrictDTO get(int id)
        {
            return mapper.Map<DistrictDTO>(districtRepository.Get(id));
        }
        [HttpDelete("{id}")]
        public void delete(int id)
        {
            District district = districtRepository.Get(id);
            district.Status = "Inactive";
            districtRepository.Update(district);
        }
        [HttpGet("")]
        public IEnumerable<DistrictDTO> search(string? name, int? numberofareas,bool all, int pageNumber,int pageSize)
        {
            if (pageNumber == 0) pageNumber = 1;
            if (pageSize == 0) pageSize = 10;
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            if (all)
            {
                return  new PagedList<District>(
                    districtRepository.GetList(null).AsQueryable(),pageNumber,pageSize)
                    .List.Select(x=> mapper.Map<DistrictDTO>(x));
                
            }
            Dictionary<int,District> dic = new Dictionary<int,District>();
            if (numberofareas != null)
            {
                IEnumerable<District> districts = districtRepository.GetList(x => x.Areas.Count > numberofareas
                 && x.Status == "Active");
                foreach (District district in districts)
                    if (!dic.ContainsKey(district.Id)) dic.Add(district.Id, district);
            }
            if (name != null)
            {
                IEnumerable<District> districts = districtRepository.GetList(x => x.Areas.Count >= numberofareas
                 && x.Status == "Active");
                foreach (District district in districts)
                    if (!dic.ContainsKey(district.Id)) dic.Add(district.Id, district);
            }
                        PagedList<District> pagedList = new PagedList<District>(dic.Values.AsQueryable(), pageNumber, pageSize);
            IEnumerable<DistrictDTO> districtDTOs = pagedList.List.Select(x => mapper.Map<DistrictDTO>(x));
            return districtDTOs;          
        }
        [HttpPut("{id}")]
        public string update(int id,[FromBody] DistrictDTO dto)
        {
            District district = districtRepository.Get(id);
            district.District1 = dto.District1;
            districtRepository.Update(district);
            return "OK";
        }
        [HttpPost("")]
        public string create([FromBody] DistrictDTO dto)
        {
            District district = mapper.Map<District>(dto);
            district.Status = "Active";
            districtRepository.Create(district);
            return "OK";
        }
    }
}
