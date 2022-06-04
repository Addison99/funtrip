using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IAreaRepository
    {
        void Create(Area Area);
        void Update(Area Area);
        void Delete(int id);
        Area Get(int id);
        IEnumerable<Area> GetList(Expression<Func<Area, bool>> func);
    }
}
