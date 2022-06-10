using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
namespace DataAccess.IRepository
{
    public interface IVehicleRepository
    {
        void Create(Vehicle Vehicle);
        void Update(Vehicle Vehicle);
        void Delete(int id);
        Vehicle Get(int id);
        IEnumerable<Vehicle> GetList(Expression<Func<Vehicle, bool>> func);
    }
}
