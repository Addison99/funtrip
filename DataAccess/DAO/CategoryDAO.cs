using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class CategoryDAO : EntityDAO<Category>
    {
        public CategoryDAO(FunTripContext dBContext) : base(dBContext)
        {
        }
    }
}
