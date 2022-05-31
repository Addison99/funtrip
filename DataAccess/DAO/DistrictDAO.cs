using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class DistrictDAO : EntityDAO<District>
    {
        public DistrictDAO(FunTripContext dBContext) : base(dBContext)
        {
        }
    }
}
