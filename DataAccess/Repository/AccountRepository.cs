using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.DAO;
using System.Linq.Expressions;
using DataAccess.Paging;

namespace DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public int GetMax()
        {
            IEnumerable<Account> accounts = GetList(null);
            int max = accounts.Max(x => x.Id);
            return max;
        }
        public Account CheckLogin(string username, string password)
            => AccountDAO.Instance.Get(x => x.Username == username && password == x.Password);

        public Account CheckLoginByMail(string mail) => AccountDAO.Instance.Get(x => x.Email == mail);

        public void Create(Account account) =>AccountDAO.Instance.Create(account);

        public void Delete(int id)
        {
            Account acc = Get(id);
            acc.Status = "Inactive";
            AccountDAO.Instance.Update(acc);    
        }

        public Account Get(int id)
        {
            return AccountDAO.Instance.Get(x => x.Id == id);
        }

        public IEnumerable<Account> GetList(Expression<Func<Account, bool>> func) => AccountDAO.Instance.GetAll(func);

        public void Update(Account account) => AccountDAO.Instance.Update(account);

        public PagedList<Account> GetAll(Expression<Func<Account, bool>> func, PagingParams pagingParams)
        {
            IQueryable<Account> list = GetList(func).AsQueryable();
            return new PagedList<Account>(list,pagingParams.PageNumber,pagingParams.PageSize);
        }
    }
}
