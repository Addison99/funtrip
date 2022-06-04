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
    public class DistrictOutsideRepository : IDistrictOutsideRepository
    {
        public void Create(DistrictOutside DistrictOutside)=> DistrictOutsideDAO.Instance.Create(DistrictOutside);

        public void Delete(int id)
        {
            DistrictOutside district = Get(id);
        }
        void AddRelations(DistrictOutside district)
        {
            if (district.City == null) district.City = CityDAO.Instance.Get(x => x.Id == district.CityId);
        }
        public DistrictOutside Get(int id)
        {
            DistrictOutside district = DistrictOutsideDAO.Instance.Get(x => id == x.Id);
            AddRelations(district);
            return district;
        }
        public IEnumerable<DistrictOutside> GetList(Expression<Func<DistrictOutside, bool>> func)
        {
            IEnumerable<DistrictOutside> districts = DistrictOutsideDAO.Instance.GetAll(func);
            foreach (DistrictOutside district in districts) AddRelations(district);
            return districts;
        }

        public void Update(DistrictOutside DistrictOutside)=> DistrictOutsideDAO.Instance.Update(DistrictOutside);
    }
}
