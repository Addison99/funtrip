using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Cost { get; set; }
        public string Feedback { get; set; }
        public int? Rate { get; set; }
        public int? VehicleId { get; set; }
        public int? DriverId { get; set; }
        public int? StartLocationId { get; set; }
        public int? EndLocationId { get; set; }
        public bool? IsRoundTrip { get; set; }
        public string Address { get; set; }
        public int? EmployeeId { get; set; }
        public string Status { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual DistrictOutside EndLocation { get; set; }
        public virtual AreaGroup StartLocation { get; set; }
        public virtual User User { get; set; }
    }
}
