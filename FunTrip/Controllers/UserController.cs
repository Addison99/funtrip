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
    public class UserController : ControllerBase
    {
        IAccountRepository _accountRepository;
        IUserRepository _userRepository;
        IMapper _mapper;
        public UserController(IAccountRepository accountRepository,IUserRepository userRepository,IMapper mapper)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public UserDTO get(int id)
        {
            return _mapper.Map<UserDTO>(_userRepository.Get(id));
        }
        [HttpGet("")]
        public IEnumerable<UserDTO> search(string? name, int? numberoforders, int pageNumber, int pageSize)
        {
            PagingParams pagingParams = new PagingParams()
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };

            Dictionary<int, User> dic = new Dictionary<int, User>();
            if (name != null)
            {
                IEnumerable<User> users = _userRepository.GetList(x=> x.FullName.Contains(name) && x.Account.Status =="Active");
                foreach(User user in users) 
                    if (!dic.ContainsKey(user.Id)) dic.Add(user.Id, user);
            }
            if (numberoforders != null)
            {
                IEnumerable<User> users = _userRepository.GetList(x => x.Orders.Count>=numberoforders && x.Account.Status == "Active");
                foreach (User user in users)
                    if (!dic.ContainsKey(user.Id)) dic.Add(user.Id, user);
            }
           PagedList<User> pagedList = new PagedList<User>(dic.Values, pageNumber, pageSize);
            IEnumerable<UserDTO> userDTOs = pagedList.List.Select(x => mapper.Map<UserDTO>(x));
            return userDTOs;
        }
        [HttpDelete("{id}")]
        public string delete(int id)
        {
            User user = _userRepository.Get(id);
            Account acc = _accountRepository.Get((int)user.AccountId);
            acc.Status = "Inactive";
            return "Delete Success";
        }
        void updateAccount(UserDTO dto,Account acc)
        {
            acc.Password = dto.Password;
            acc.Email = dto.Email;
            acc.Status = "Active";
        }
        [HttpPost("")]
        public string create([FromBody] UserDTO dto)
        {
            User user = _mapper.Map<User>(dto);
            Account acc = new Account();
            acc.Username = dto.Username;
            updateAccount(dto, acc);
            _accountRepository.Create(acc);
            int id = _accountRepository.GetMax();
            user.AccountId = id;
            _userRepository.Create(user);
            return "Create Success";
        }
        [HttpPut("")]
        public string update([FromBody] UserDTO dto)
        {
            User user = _mapper.Map<User>(dto);
            Account account = _accountRepository.Get((int)user.AccountId);
            updateAccount(dto, account);
            user.Account = account;
            _userRepository.Update(user);
            return "Update Success";
        }
    }
}
