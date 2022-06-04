using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IOrderRepository
    {
        void Create(Order Order);
        void Update(Order Order);
        void Delete(int id);
        Order Get(int id);
        IEnumerable<Order> GetList(Expression<Func<Order, bool>> func);
    }
}
