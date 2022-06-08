using BusinessObject.Models;
using DataAccess.Paging;
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
        int GetMax();
        IEnumerable<Account> GetList(Expression<Func<Account, bool>> func);
        PagedList<Account> GetAll(Expression<Func<Account, bool>> func,PagingParams pagingParams);
    }
}
