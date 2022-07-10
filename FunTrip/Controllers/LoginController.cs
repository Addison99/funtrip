﻿using DataAccess.IRepository;
using FunTrip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BusinessObject.Models;
using DataAccess.Repository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace FunTrip.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IUserRepository userRepository;
        private IAccountRepository accountRepository;
        private SignInManager<Account> signInManager;
        public LoginController(IConfiguration config,IAccountRepository accountRepository
            ,IUserRepository userRepository)
        {
            _config = config;
            this.userRepository = new UserRepository();
            this.accountRepository = new AccountRepository();
            
        }
        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }
        private string Generate(Account user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            String role;
            if (user.RoleId == 1) role = "Admin";
            else if (user.RoleId == 2) role = "Employee";
            else if (user.RoleId == 3) role = "Driver";
            else role = "Customer";
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(String.Empty,
              String.Empty,
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private Account Authenticate(UserLogin userLogin)
        {
            
            var currentUser = accountRepository.CheckLogin(userLogin.Username,userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
