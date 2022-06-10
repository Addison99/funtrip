using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class City
    {
        public City()
        {
            DistrictOutsides = new HashSet<DistrictOutside>();
        }

        public int Id { get; set; }
        public string City1 { get; set; }
        public string Status { get; set; }

        public virtual ICollection<DistrictOutside> DistrictOutsides { get; set; }
    }
}
