using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class EmployeeDAO : EntityDAO<Employee>
    {
        public EmployeeDAO(FunTripContext dBContext) : base(dBContext)
        {
        }
    }
}
