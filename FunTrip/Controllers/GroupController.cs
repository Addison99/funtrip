using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.IRepository;
using DataAccess.Repository;
using BusinessObject.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FunTrip.DTOs;
using DataAccess.Paging;
using AutoMapper;

namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        IGroupRepository groupRepository { get; set; }
        IMapper mapper { get; set; }
        public GroupController(IGroupRepository groupRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public GroupDTO get(int id)
        {
            return mapper.Map<GroupDTO>(groupRepository.Get(id));
        }
        [HttpDelete("{id}")]
        public void delete(int id)
        {
            Group group = groupRepository.Get(id);
            group.Status = "Active";
            groupRepository.Update(group);
        }
        [HttpGet("")]
        public IEnumerable<GroupDTO> search(int? NumberOfMembers, int? NumberOfAreas, string? GroupName, int pageNumber, int pageSize)
        {
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            Dictionary<int, Group> dic = new Dictionary<int, Group>();
            if (NumberOfMembers != null)
            {
                IEnumerable<Group> groups = groupRepository.GetList(x => x.Drivers.Count >= NumberOfMembers
                    && x.Status == "Active");
                foreach (Group group in groups)
                    if (!dic.ContainsKey(group.Id))
                {
                    dic.Add(group.Id, group);
                }
            }
            if (NumberOfAreas != null)
            {
                IEnumerable<Group> groups = groupRepository.GetList(x => x.AreaGroups.Count >= NumberOfAreas
                    && x.Status == "Active");
                foreach (Group group in groups)
                    if (!dic.ContainsKey(group.Id))
                    {
                        dic.Add(group.Id, group);
                    }
            }
            if (GroupName != null)
            {
                IEnumerable<Group> groups = groupRepository.GetList(x => x.GroupName.Contains(GroupName)
                    && x.Status == "Active");
                foreach (Group group in groups)
                    if (!dic.ContainsKey(group.Id))
                    {
                        dic.Add(group.Id, group);
                    }
            }
                    PagedList<Group> pagedList = new PagedList<Group>(dic.Values.AsQueryable(), pageNumber, pageSize);
            IEnumerable<GroupDTO> groupDTOs = pagedList.List.Select(x => mapper.Map<GroupDTO>(x));
            return groupDTOs;
        }
        [HttpPut("")]
        public string update([FromBody] GroupDTO dto)
        {
            Group group = groupRepository.Get(dto.Id);
            group.Phone = dto.Phone;
            group.GroupName = dto.GroupName;
            group.ManagerId = dto.ManagerId;
            group.ApartmentId = dto.ApartmentId;
            groupRepository.Update(group);
            return "OK";
        }
        [HttpPost("")]
        public string create([FromBody] GroupDTO dto)
        {
            Group group = mapper.Map<Group>(dto);
            group.Status = "Active";
            groupRepository.Create(group);
            return "OK";
        }
    }
}
