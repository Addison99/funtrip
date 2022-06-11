using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.IRepository;
using DataAccess.Repository;
using BusinessObject.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FunTrip.DTOs;
using System;
using DataAccess.Paging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IAccountRepository accountRepository;
        IEmployeeRepository employeeRepository;
        IMapper mapper;
        public EmployeeController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.accountRepository = accountRepository;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
            
        [HttpGet("{id}")]
        public EmployeeDTO Get(int id)
        {
            Employee employee = employeeRepository.Get(id);
            return mapper.Map<EmployeeDTO>(employee);
        }
        [HttpGet("{name}")]
        public IEnumerable<EmployeeDTO> getlist(string name, int pageNumber, int pageSize)
        {
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            PagedList<Employee> pagedList = new PagedList<Employee>(employeeRepository.GetList(x=> x.FullName.Contains(name) && x.Account.Status =="Active").AsQueryable(), pageNumber, pageSize);
            IEnumerable<EmployeeDTO> employeeDTOs = pagedList.List.Select(x => mapper.Map<EmployeeDTO>(x));
            return employeeDTOs; 
        }
        [HttpDelete("{id}")]
        public string delete(int id)
        {
            Employee employee = employeeRepository.Get(id);
            employee.Account = accountRepository.Get((int)employee.AccountId);
            employee.Account.Status = "Inactive";
            accountRepository.Update(employee.Account);
            return "Delete Success";
        }
        [HttpPost("")]
        public string create([FromBody] EmployeeDTO dto)
        {
            Employee employee = mapper.Map<Employee>(dto);
            Account account = new Account()
            {
                Username = dto.Username,
                Email = dto.Gmail,
                Password = dto.Password,
                Status = "Active"
            };
            try
            {
                accountRepository.Create(account);
                int id = accountRepository.GetMax();
                employee.AccountId = id;
                employeeRepository.Create(employee);
            }catch (Exception ex)
            {
                return ex.Message;
            }
            return "Success";
        }
        [HttpPut("")]
        public string update([FromBody] EmployeeDTO dto)
        {
            Employee employee = mapper.Map<Employee>(dto);
            try
            {
                Account acc = accountRepository.Get((int)employee.AccountId);
                acc.Password = dto.Password;
                acc.Email = dto.Gmail;
                employee.Account = acc;
                employeeRepository.Update(employee);
            }catch(Exception ex)
            {
                return ex.Message;
            }
            return "Success";
        }
        
    }
}
