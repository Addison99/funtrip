namespace FunTrip.DTOs
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int ManagerId { get; set; }
        public int? ApartmentId { get; set; }
        public string Phone { get; set; }
        public int NumberOfMembers { get; set; }
        public int NumberOfAreas { get; set; }
    }
}
