using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.DAO;
namespace DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Account CheckLogin(string username, string password)
            => AccountDAO.Instance.Get(x => x.Username == username && password == x.Password);

        public Account CheckLoginByMail(string mail) => AccountDAO.Instance.Get(x => x.Email == mail);
    }
}
