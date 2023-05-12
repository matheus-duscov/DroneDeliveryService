using DroneDeliveryServiceMvc.Models;

namespace DroneDeliveryServiceMvc.Services.Trip
{
    public interface ITripService
    {
        List<Models.Trip> MakeTripForAllDrones(List<Drone> drones, List<Location> locations);
        Models.Trip MakeTripItinerary(int id, Drone drone, List<Location> locations);
        Location GetLocation(int currentWeightCapacity, List<Location> locations);
    }
}
