using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.IRepository;
using DataAccess.Repository;
using BusinessObject.Models;

namespace FunTrip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountRepository _accountRepository;

    }
}
