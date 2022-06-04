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
        public void Create(Order Order)=> OrderDAO.Instance.Create(Order);

        public void Delete(int id)
        {
            Order order = Get(id);
            order.Status = "Inactive";
        }
        void AddRalations(Order order)
        {
            if (order == null) return;
            if (order.Driver == null) order.Driver = DriverDAO.Instance.Get(x => x.Id == order.DriverId);
            if (order.Employee ==null) order.Employee = EmployeeDAO.Instance.Get(x => x.Id == order.EmployeeId);
            if (order.StartLocation ==null) 
                order.StartLocation = AreaGroupDAO.Instance.Get(x => x.Id == order.StartLocationId);
            if (order.EndLocation ==null) 
                order.EndLocation = DistrictOutsideDAO.Instance.Get(x => x.Id == order.EndLocationId);
        }
        public Order Get(int id)
        {
            Order order = OrderDAO.Instance.Get(x => x.Id == id);
            AddRalations(order);
            return order;
        }

        public IEnumerable<Order> GetList(Expression<Func<Order, bool>> func)
        {
            IEnumerable<Order> orders = OrderDAO.Instance.GetAll(func); 
            foreach (Order order in orders) AddRalations(order);
            return orders;
        }

        public void Update(Order Order) => OrderDAO.Instance.Update(Order);

    }
}
