using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.DAO;
using System.Linq.Expressions;
namespace DataAccess.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public void Create(Role Role)=> RoleDAO.Instance.Create(Role);

        public void Delete(int id)
        {
            Role role = Get(id);
            role.Status = "Inactive";
        }

        public Role Get(int id)=>RoleDAO.Instance.Get(x=>x.Id==id); 

        public IEnumerable<Role> GetList(Expression<Func<Role, bool>> func)
        {
            throw new NotImplementedException();
        }

        public void Update(Role Role)
        {
            throw new NotImplementedException();
        }
    }
}
