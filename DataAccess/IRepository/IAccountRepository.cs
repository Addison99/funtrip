using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.IRepository
{
    public interface IAccountRepository
    {
        Account CheckLogin(string username, string password);
        Account CheckLoginByMail(String mail);
    }
}
