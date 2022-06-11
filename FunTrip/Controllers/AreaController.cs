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
using DataAccess.Paging;

namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        IAreaRepository areaRepository;
        IMapper mapper;
        public AreaController (IMapper mapper, IAreaRepository areaRepository)
        {
            this.mapper = mapper;
            this.areaRepository = areaRepository;
        }
        [HttpGet("{id}")]
        public AreaDTO get(int id)
        {
            return mapper.Map<AreaDTO>(areaRepository.Get(id));
        }
         [HttpGet("{pageNumber}/{pageSize}")]
        public IEnumerable<AreaDTO> GetList(string? ApartmentName,string? Address,int? DistrictId, bool? all ,int pageNumber,int pageSize)
                {
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            PagedList<Area> pagedList = new PagedList<Area>(areaRepository.GetList(null).AsQueryable(), pageNumber, pageSize);
            IEnumerable<AreaDTO> areaDTOs = pagedList.List.Select
                (
                    x => mapper.Map<AreaDTO>(x)
                    );
            return areaDTOs;
        }

        [HttpDelete("{id}")]
        public string delete(int id)
        {
            Area area = areaRepository.Get(id);
            area.Status = "Inactive";
            return "Delete success";
        }
        [HttpGet("")]
        public IEnumerable<AreaDTO> search(string? ApartmentName, string? Address, int? DistrictId, string? DistrictName, int? numberOfGroups, bool? all ,int pageNumber,int pageSize)
        {
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            Dictionary<int, Area> dic = new Dictionary<int, Area>();
            if (ApartmentName != null)
            {
                IEnumerable<Area> areas = areaRepository.GetList(x => x.ApartmentName.Contains(ApartmentName)
                    && x.Status == "Active");
                foreach (Area area in areas)
                    if (!dic.ContainsKey(area.Id)) dic.Add(area.Id, area);
            }
            if (DistrictName != null)
            {
                IEnumerable<Area> areas = areaRepository.GetList(x => x.District.District1.Contains(DistrictName)
                    && x.Status == "Active");
                foreach (Area area in areas)
                    if (!dic.ContainsKey(area.Id)) dic.Add(area.Id, area);
            }
            if (numberOfGroups != null)
            {
                IEnumerable<Area> areas = areaRepository.GetList(x => x.AreaGroups.Count > numberOfGroups
                    && x.Status == "Active");
                foreach (Area area in areas)
                    if (!dic.ContainsKey(area.Id)) dic.Add(area.Id, area);
            }
            return dic.Values.Select(x => mapper.Map<AreaDTO>(x));
            PagedList<Area> pagedList = new PagedList<Area>(dic.Values.AsQueryable(),pageNumber,pageSize);
            IEnumerable<AreaDTO> listDTO = pagedList.List.Select
                (
                    x => mapper.Map<AreaDTO>(x)
                    );
        }
        
        [HttpPost("")]
        public string create([FromBody] AreaDTO dto)
        {
            Area area = mapper.Map<Area>(dto);
            area.Status = "Active";
            areaRepository.Create(area);
            return "Create Success";
        }
        [HttpPut("")]
        public string update([FromBody] AreaDTO dto)
        {
            Area area = areaRepository.Get(dto.Id);
            try
            {
                area.Address = dto.Address;
                area.ApartmentName = dto.ApartmentName;
                area.DistrictId = dto.DistrictId;
                areaRepository.Update(area);
            }catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            return "Update Success";
        }
        
    }
}
