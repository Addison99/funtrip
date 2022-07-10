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
    public class OrderRepository : IOrderRepository
    {
        public void Create(Booking Order)=> BookingDAO.Instance.Create(Order);

        public void Delete(int id)
        {
            Booking order = Get(id);
            order.Status = "Inactive";
        }
        void AddRalations(Booking order)
        {
            if (order == null) return;
            if (order.Driver == null) order.Driver = DriverDAO.Instance.Get(x => x.Id == order.DriverId);
            if (order.Employee ==null) order.Employee = EmployeeDAO.Instance.Get(x => x.Id == order.EmployeeId);
            if (order.StartLocation ==null) 
                order.StartLocation = AreaGroupDAO.Instance.Get(x => x.Id == order.StartLocationId);
           
        }
        public Booking Get(int id)
        {
            Booking order = BookingDAO.Instance.Get(x => x.Id == id);
            AddRalations(order);
            return order;
        }

        public IEnumerable<Booking> GetList(Expression<Func<Booking, bool>> func)
        {
            IEnumerable<Booking> orders = BookingDAO.Instance.GetAll(func); 
            foreach (Booking order in orders) AddRalations(order);
            return orders;
        }

        public IEnumerable<Booking> GetAllBookings() => BookingDAO.Instance.GetAll(null, "Driver");

        public void Update(Booking Order) => BookingDAO.Instance.Update(Order);

    }
}
