namespace FunTrip.DTOs
{
    public class DriverDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gmail { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CreditCard { get; set; }
        public int? GroupId { get; set; }
        public int? VehicleId { get; set; }
        public int? AccountId { get; set; }
        public string GroupName { get; set; }
        public string VehicleName { get; set; }
        public float rate { get; set; }
        public string Email { get; set; }
    }
}
