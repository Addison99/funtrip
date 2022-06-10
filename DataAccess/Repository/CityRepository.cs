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
    public class CityRepository : ICityRepository
    {
        public void Create(City City)=> CityDAO.Instance.Create(City);

        public void Delete(int id)
        {
            City city = Get(id);
        }
        void AddRelations(City city)
        {
            if (city.DistrictOutsides==null) city.DistrictOutsides = DistrictOutsideDAO.Instance.GetAll(x => x.CityId == city.Id).ToList();
        }
        public City Get(int id)
        {
            City city = CityDAO.Instance.Get(x => x.Id == id);
            if (city != null) AddRelations(city);
            return city;
        }
        public IEnumerable<City> GetList(Expression<Func<City, bool>> func)
        {
            IEnumerable<City> cities = CityDAO.Instance.GetAll(func); 
            foreach (City city in cities) AddRelations(city);
            return cities;
        }

        public void Update(City City)=> CityDAO.Instance.Update(City);  
    }
}
