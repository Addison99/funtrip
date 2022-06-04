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
    public class DistrictRepository : IDistrictRepository
    {
        public void Create(District District)=>DistrictDAO.Instance.Create(District);
        public void Delete(int id)
        {
            District district = Get(id);
        }
        void AddRelations(District district)
        {
            if (district == null) return;
            if (district.Areas == null) district.Areas = AreaDAO.Instance.GetAll(x => x.DistrictId == district.Id).ToList();
        }
        public District Get(int id)
        {
            District district = DistrictDAO.Instance.Get(x => x.Id == id); 
            AddRelations(district);
            return district;
        }

        public IEnumerable<District> GetList(Expression<Func<District, bool>> func)
        {
            IEnumerable<District> districts = DistrictDAO.Instance.GetAll(func);
            foreach (District district in districts) AddRelations(district);
            return districts;
        }

        public void Update(District District)=> DistrictDAO.Instance.Update(District);
    }
}
