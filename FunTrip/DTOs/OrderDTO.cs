using System;

namespace FunTrip.DTOs
{
    public class OrderDTO
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

        public string DriverName { get; set; }
        public string EmployeeName { get; set; }
        public string StartLoction { get; set; }
        public string EndLocation { get; set; }
        public string UserName { get;set; }
        public String GroupName { get; set; }

        public string DistrictOutsideName { get; set; }

    }
}
