using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string VehicleName { get; set; }
        public string Manufacturer { get; set; }
        public int CategoryId { get; set; }
        public int? GroupId { get; set; }
        public string Status { get; set; }
        public string Img { get; set; }
        public int? DriverId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
