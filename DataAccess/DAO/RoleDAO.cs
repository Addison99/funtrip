using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class RoleDAO : EntityDAO<Role>
    {
        public RoleDAO(FunTripContext dBContext) : base(dBContext)
        {
        }
    }
}
