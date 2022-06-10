using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IDistrictRepository
    {
        void Create(District District);
        void Update(District District);
        void Delete(int id);
        District Get(int id);
        IEnumerable<District> GetList(Expression<Func<District, bool>> func);
    }
}
