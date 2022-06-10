using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IDriverRepository
    {
        void Create(Driver Driver);
        void Update(Driver Driver);
        void Delete(int id);
        Driver Get(int id);
        IEnumerable<Driver> GetList(Expression<Func<Driver, bool>> func);
    }
}
