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
    [Route("api/areas")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        IAreaRepository areaRepository;
        IDistrictRepository districtRepository;
        IMapper mapper;
        public AreaController (IMapper mapper, IAreaRepository areaRepository, IDistrictRepository districtRepository)
        {
            this.mapper = mapper;
            this.areaRepository = areaRepository;
            this.districtRepository = districtRepository;
        }
        [HttpGet("{id}")]
        public AreaDTO get(int id)
        {
            return mapper.Map<AreaDTO>(areaRepository.Get(id));
        }
        [HttpDelete("{id}")]
        public string delete(int id)
        {
            Area area = areaRepository.Get(id);
            area.Status = "Inactive";
            areaRepository.Update(area);    
            return "Delete success";
        }
        [HttpGet("")]
        public IEnumerable<AreaDTO> search(string? ApartmentName, string? Address, int? DistrictId, string? DistrictName, int? numberOfGroups, bool? all ,int pageNumber,int pageSize)
        {
            if (pageNumber == 0) pageNumber = 1;
            if (pageSize == 0) pageSize = 10;

            if (all==true)
            {
                PagedList<Area> pagedList1 = new PagedList<Area>(areaRepository.GetList(x => x.Status == "Active").AsQueryable()
                    , pageNumber, pageSize);
                IEnumerable<AreaDTO> listDTO1 = pagedList1.List.Select
                (
                    x => mapper.Map<AreaDTO>(x)
                    );
                return listDTO1;
            }
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
            
            PagedList<Area> pagedList = new PagedList<Area>(dic.Values.AsQueryable(),pageNumber,pageSize);
            IEnumerable<AreaDTO> listDTO = pagedList.List.Select
                (
                    x => mapper.Map<AreaDTO>(x)
                    );
            return listDTO;
        }
        
        [HttpPost("")]
        public string create([FromBody] AreaDTO dto)
        {
            Area area = mapper.Map<Area>(dto);
            area.Status = "Active";
            areaRepository.Create(area);
            return "Create Success";
        }
        [HttpPut("{id}")]
        public string update([FromRoute]int id,[FromBody] AreaDTO dto)
        {
            Area area = areaRepository.Get(id);
            try
            {
                area.Address = dto.Address;
                area.ApartmentName = dto.ApartmentName;
                area.DistrictId = dto.DistrictId;
                area.District = districtRepository.Get((int)dto.DistrictId);
                areaRepository.Update(area);
            }catch(Exception ex)
            {
                return ex.Message.ToString();
            }
            return "Update Success";
        }
        
    }
}
