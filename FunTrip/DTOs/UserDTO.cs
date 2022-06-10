namespace FunTrip.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? AccountId { get; set; }
        public string Creditcard { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
