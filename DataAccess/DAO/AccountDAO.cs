using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO : EntityDAO<Account>
    {
        public AccountDAO(FunTripContext dBContext) : base(dBContext)
        {
        }
    }
}
