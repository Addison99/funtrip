namespace FunTrip.DTOs
{
    public class AreaDTO
    {
        public int Id { get; set; }
        public string ApartmentName { get; set; }
        public string Address { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int NumberOfGroups { get; set; }
    }
}
