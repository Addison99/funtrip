using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Group
    {
        public Group()
        {
            Drivers = new HashSet<Driver>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public int ManagerId { get; set; }
        public int? ApartmentId { get; set; }
        public string Phone { get; set; }

        public virtual Area Apartment { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
