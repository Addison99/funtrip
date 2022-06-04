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
    public class DriverRepository : IDriverRepository
    {
        public void Create(Driver Driver)=> DriverDAO.Instance.Create(Driver);  
        public void Delete(int id)
        {
            Driver driver = Get(id);
        }
        void AddRelations(Driver driver)
        {
            if (driver == null) return;
            if (driver.Group == null) driver.Group = GroupDAO.Instance.Get(x => x.Id == driver.GroupId);
            if (driver.Vehicle ==null) driver.Vehicle = VehicleDAO.Instance.Get(x => x.Id == driver.VehicleId);
            if (driver.Orders == null) driver.Orders = OrderDAO.Instance.GetAll(x => x.DriverId == driver.Id).ToList();
            if (driver.Account == null) driver.Account = AccountDAO.Instance.Get(x => x.Id == driver.AccountId);
        }
        public Driver Get(int id)
        {
            Driver driver = DriverDAO.Instance.Get(x => x.Id == id);
            if (driver!=null) AddRelations(driver);
            return driver;  
        }

        public IEnumerable<Driver> GetList(Expression<Func<Driver, bool>> func)
        {
            IEnumerable<Driver> drivers = DriverDAO.Instance.GetAll(func).ToList();
            foreach(Driver driver in drivers) AddRelations(driver);
            return drivers;
        }

        public void Update(Driver Driver)=> DriverDAO.Instance.Update(Driver);
    }
}
