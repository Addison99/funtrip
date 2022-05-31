using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class DistrictOutsideDAO : EntityDAO<DistrictOutside>
    {
        public DistrictOutsideDAO(FunTripContext dBContext) : base(dBContext)
        {
        }
    }
}
