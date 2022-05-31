using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class GroupDAO : EntityDAO<Group>
    {
        public GroupDAO(FunTripContext dBContext) : base(dBContext)
        {
        }
    }
}
