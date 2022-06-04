namespace FunTrip.DTOs
{
    public class DistrictOutsideDTO
    {
        public int Id { get; set; }
        public string District { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
    }
}
