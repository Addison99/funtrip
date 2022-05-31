using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Area
    {
        public Area()
        {
            Groups = new HashSet<Group>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string ApartmentName { get; set; }
        public string Address { get; set; }
        public int? DistrictId { get; set; }

        public virtual District District { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
