using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IUserRepository
    {
        void Create(User User);
        void Update(User User);
        void Delete(int id);
        User Get(int id);
        IEnumerable<User> GetList(Expression<Func<User, bool>> func);
    }
}
