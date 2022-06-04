using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using BusinessObject.Models;
namespace DataAccess.IRepository
{
    public interface ICityRepository
    {
        void Create(City City);
        void Update(City City);
        void Delete(int id);
        City Get(int id);
        IEnumerable<City> GetList(Expression<Func<City, bool>> func);
    }
}
