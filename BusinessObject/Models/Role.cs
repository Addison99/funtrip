using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Role1 { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
