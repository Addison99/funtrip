using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Category
    {
        public Category()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string Category1 { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
