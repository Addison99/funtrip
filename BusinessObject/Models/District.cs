using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class District
    {
        public District()
        {
            Areas = new HashSet<Area>();
        }

        public int Id { get; set; }
        public string District1 { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
    }
}
