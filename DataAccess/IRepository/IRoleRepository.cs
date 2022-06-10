using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IRoleRepository
    {
        void Create(Role Role);
        void Update(Role Role);
        void Delete(int id);
        Role Get(int id);
        IEnumerable<Role> GetList(Expression<Func<Role, bool>> func);
    }
}
