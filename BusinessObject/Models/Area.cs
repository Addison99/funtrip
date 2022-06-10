using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Area
    {
        public Area()
        {
            AreaGroups = new HashSet<AreaGroup>();
        }

        public int Id { get; set; }
        public string ApartmentName { get; set; }
        public string Address { get; set; }
        public int? DistrictId { get; set; }
        public string Status { get; set; }

        public virtual District District { get; set; }
        public virtual ICollection<AreaGroup> AreaGroups { get; set; }
    }
}
