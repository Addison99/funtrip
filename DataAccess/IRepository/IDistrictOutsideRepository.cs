using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IDistrictOutsideRepository
    {
        void Create(DistrictOutside DistrictOutside);
        void Update(DistrictOutside DistrictOutside);
        void Delete(int id);
        DistrictOutside Get(int id);
        IEnumerable<DistrictOutside> GetList(Expression<Func<DistrictOutside, bool>> func);
    }
}
