namespace BookingERP.Data.Entities
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public List<Room> RoomList { get; set; }
        public List<Manager> ManagersList { get; set; }

    }
}
