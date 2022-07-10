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
    public class EmployeeRepository : IEmployeeRepository
    {
        public int getMax()
        {
            IEnumerable<Employee> employees = GetList(null);
            return employees.Max(x => x.Id) + 1;
        }
        public void Create(Employee Employee)=> EmployeeDAO.Instance.Create(Employee);

        public void Delete(int id)
        {
            Employee employee = Get(id);
        }

        public Employee Get(int id)
        {
            return EmployeeDAO.Instance.Get(x => x.Id == id);
        }

        public IEnumerable<Employee> GetList(Expression<Func<Employee, bool>> func)=> EmployeeDAO.Instance.GetAll(func);

        public void Update(Employee Employee)=>EmployeeDAO.Instance.Update(Employee);   
    }
}
