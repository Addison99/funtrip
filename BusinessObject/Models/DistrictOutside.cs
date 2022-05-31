using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class DistrictOutside
    {
        public DistrictOutside()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string District { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
