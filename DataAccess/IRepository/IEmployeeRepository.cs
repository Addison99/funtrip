using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using System.Linq.Expressions;
namespace DataAccess.IRepository
{
    public interface IEmployeeRepository
    {
        void Create(Employee Employee);
        void Update(Employee Employee);
        void Delete(int id);
        Employee Get(int id);
        IEnumerable<Employee> GetList(Expression<Func<Employee, bool>> func);
    }
}
