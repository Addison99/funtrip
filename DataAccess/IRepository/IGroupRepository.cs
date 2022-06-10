using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IGroupRepository
    {
        void Create(Group Group);
        void Update(Group Group);
        void Delete(int id);
        Group Get(int id);
        IEnumerable<Group> GetList(Expression<Func<Group, bool>> func);
    }
}
