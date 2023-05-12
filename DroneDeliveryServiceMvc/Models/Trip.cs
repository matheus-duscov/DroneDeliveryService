namespace DroneDeliveryServiceMvc.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public Drone Drone { get; set; } = new Drone();
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
