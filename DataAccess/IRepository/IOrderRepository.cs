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
        void Create(Booking rder);
        void Update(Booking Order);
        void Delete(int id);
        Booking Get(int id);
        IEnumerable<Booking> GetList(Expression<Func<Booking, bool>> func);
    }
}
