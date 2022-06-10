using FunTrip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using DataAccess.IRepository;
using DataAccess.Repository;
using BusinessObject.Models;
using DataAccess.DAO;
namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuController : ControllerBase
    {
        [HttpGet("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Email}");
        }


        [HttpGet("Customer")]
        [Authorize(Roles = "Customer")]
        public IActionResult SellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Email}");
        }

        [HttpGet("Driver")]
        [Authorize(Roles = "Driver")]
        public IActionResult AdminsAndSellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Email}");
        }
        [HttpGet("Employee")]
        [Authorize(Roles = "Employee")]
        public IActionResult EmployeeEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Email}");
        }
        IAccountRepository accountRepository;
        [HttpGet("Public")]
        public IActionResult Public()
        {
            //accountRepository = new AccountRepository();
            //Account acc = accountRepository.CheckLogin("son", "123");
            FunTripContext context = new FunTripContext();
            Account account = context.Accounts.FirstOrDefault(x=> x.Id == 1);
            context = null;
            return Ok("Hi, you're on public property "+ account.Email);
        }

        private Account GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new Account
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                };
            }
            return null;
        }
    }
}
