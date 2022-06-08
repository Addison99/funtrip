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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountRepository _accountRepository { get; set; }
        IMapper mapper { get; set; }
        public AccountController(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]

        public AccountDTO Get(int id)

        {
            Account account = _accountRepository.Get(id);
            AccountDTO accountDTO;
            
            accountDTO = mapper.Map<AccountDTO>(account);
            return accountDTO;
        }
        [HttpGet("{pageNumber}/{pageSize}")]
        public IEnumerable<AccountDTO> GetList(string? username,string? password,string? gmail, bool? all ,int pageNumber,int pageSize)
        {
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            Dictionary<int,Account> dic = new Dictionary<int,Account>();
            if (all == null)
            {
                if (username != null && password != null)
                {
                    List<Account> list = _accountRepository.GetList(x => x.Username == username
                        && x.Password == password && x.Status == "Active").ToList();
                    foreach (Account account in list)
                        if (!dic.ContainsKey(account.Id)) dic.Add(account.Id, account);

                }
                if (gmail != null)
                {
                    List<Account> list = _accountRepository.GetList(x => x.Email == gmail && x.Status == "Active").ToList();
                    foreach (Account account in list)
                        if (!dic.ContainsKey(account.Id)) dic.Add(account.Id, account);
                }
            }
            else
            {
                List<Account> list = _accountRepository.GetList(x=> x.Status == "Active").ToList();
                foreach (Account account in list)
                    if (!dic.ContainsKey(account.Id)) dic.Add(account.Id, account);
            }
            PagedList<Account> pagedList = new PagedList<Account>(dic.Values.AsQueryable(),pageNumber,pageSize);
            IEnumerable<AccountDTO> listDTO = pagedList.List.Select
                (
                    x => mapper.Map<AccountDTO>(x)
                    );
            return listDTO;
        }
        
    }

}
