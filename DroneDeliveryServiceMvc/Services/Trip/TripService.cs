using DroneDeliveryServiceMvc.Models;
using System.Text;

namespace DroneDeliveryServiceMvc.Services.Trip
{
    public class TripService : ITripService
    {
        public List<Models.Trip> MakeTripForAllDrones(List<Drone> drones, List<Location> locations)
        {
            var result = new List<Models.Trip>();
            int tripId = 1;

            while(locations.Count > 0)
            {
                foreach (var drone in drones.OrderByDescending(x => x.MaximumWeight))
                {
                    var trip = MakeTripItinerary(tripId, drone, locations);
                    locations = RemoveUsedLocations(trip, locations);
                    result.Add(trip);

                    if (locations.Count == 0)
                        break;
                }
                tripId++;
            }

            return result;
        }

        public Models.Trip MakeTripItinerary(int id, Drone drone, List<Location> locations)
        {
            var trip = new Models.Trip
            {
                Id = id,
                Drone = drone
            };

            var location = new Location();
            var locationList = new List<Location>();
            int currentWeightCapacity = trip.Drone.MaximumWeight;

            while (location != null)
            {
                location = GetLocation(currentWeightCapacity, locations);
                if (location != null)
                {
                    locationList.Add(location);
                    currentWeightCapacity -= location.PackageWeight;
                    locations.Remove(location);
                }
            }

            trip.Locations = locationList;

            return trip;
        }        

        public Location GetLocation(int currentWeightCapacity, List<Location> locations)
        {
            var location = new Location();

            location = locations.Where(x => currentWeightCapacity >= x.PackageWeight).MaxBy(x => x.PackageWeight);
            if (location != null)
                return location;

            location = locations.Where(x => currentWeightCapacity >= x.PackageWeight).MinBy(x => x.PackageWeight);
            if (location != null)
                return location;

            return location;
        }

        private List<Location> RemoveUsedLocations(Models.Trip trip, List<Location> locations)
        {
            foreach (var location in trip.Locations)
            {
                locations.Remove(location);
            }

            return locations;
        }
    }
}
