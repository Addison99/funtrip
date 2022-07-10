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
    public class VehicleRepository : IVehicleRepository
    {
        public void Create(Vehicle Vehicle)=> VehicleDAO.Instance.Create(Vehicle);

        public void Delete(int id)
        {
            Vehicle vehicle = Get(id);
        } 
        void AddRelations(Vehicle vehicle)
        {
            if (vehicle == null) return;
            if (vehicle.Driver == null) vehicle.Driver = DriverDAO.Instance.Get(x => x.VehicleId == vehicle.Id);
            if (vehicle.Category == null) vehicle.Category = CategoryDAO.Instance.Get(x =>x.Id == vehicle.CategoryId);
        }
        public Vehicle Get(int id)
        {
            Vehicle v = VehicleDAO.Instance.Get(x => x.Id == id); 
            AddRelations(v);
            return v;
        }

        public IEnumerable<Vehicle> GetList(Expression<Func<Vehicle, bool>> func)
        {
            IEnumerable<Vehicle> vehicles = VehicleDAO.Instance.GetAll(func);
            foreach(Vehicle vehicle in vehicles) AddRelations(vehicle);
            return vehicles;
        }

        public void Update(Vehicle Vehicle)
        {
            VehicleDAO.Instance.Update(Vehicle);    
        }
    }
}
