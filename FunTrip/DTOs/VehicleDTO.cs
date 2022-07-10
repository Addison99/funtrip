    namespace FunTrip.DTOs
{
    public class VehicleDTO
    {
        public int? Id { get; set; }
        public string VehicleName { get; set; }
        public string Manufacturer { get; set; }
        public int? CategoryId { get; set; }
        public int? GroupId { get; set; }
        public string Img { get; set; }
        public int? DriverId { get; set; }
        public string DriverName { get; set; }
        public string CategoryName { get; set; }
    }
}
