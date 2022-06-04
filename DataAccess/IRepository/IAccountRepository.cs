using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.IRepository
{
    public interface IAccountRepository
    {
        Account CheckLogin(string username, string password);
        Account CheckLoginByMail(String mail);
        void Create(Account account);
        void Update(Account account);
        void Delete(int id);
        Account Get(int id);
        IEnumerable<Account> GetList(Expression<Func<Account, bool>> func);
    }
}
