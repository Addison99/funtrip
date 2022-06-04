using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gmail { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CreditCard { get; set; }
        public int? GroupId { get; set; }
        public int? VehicleId { get; set; }
        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Group Group { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
