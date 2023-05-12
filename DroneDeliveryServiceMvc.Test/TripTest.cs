using DroneDeliveryServiceMvc.Models;

namespace DroneDeliveryServiceMvc.Test
{
    public class TripTest : BaseTester
    {
        [Theory]
        [InlineData("DroneTest", 300)]
        [InlineData("DroneTest", 290)]
        [InlineData("DroneTest", 280)]
        [InlineData("DroneTest", 270)]
        public void GetLocationTest1(string droneName, int maximumWeight)
        {
            var drone = new Drone
            {
                Name = droneName,
                MaximumWeight = maximumWeight
            };
            var listString = ListStringFile1();
            var locationListFromFile = _processFileService.CreateListLocation(listString);

            var trip = new Trip
            {
                Id = 1,
                Drone = drone
            };

            var location = new Location();
            var locationList = new List<Location>();
            int currentWeightCapacity = drone.MaximumWeight;

            while (location != null)
            {
                location = _tripService.GetLocation(currentWeightCapacity, locationListFromFile);
                if (location != null)
                {
                    locationList.Add(location);
                    currentWeightCapacity -= location.PackageWeight;
                    locationListFromFile.Remove(location);
                }
            }

            trip.Locations = locationList;

            Assert.Equal(maximumWeight, trip.Locations.Sum(x => x.PackageWeight));
        }

        [Theory]
        [InlineData("DroneTest", 260)]
        public void GetLocationTest2(string droneName, int maximumWeight)
        {
            var drone = new Drone
            {
                Name = droneName,
                MaximumWeight = maximumWeight
            };
            var listString = ListStringFile1();
            var locationListFromFile = _processFileService.CreateListLocation(listString);
            
            var trip = new Trip
            {
                Id = 1,
                Drone = drone
            };

            var location = new Location();
            var locationList = new List<Location>();
            int currentWeightCapacity = drone.MaximumWeight;

            while (location != null)
            {
                location = _tripService.GetLocation(currentWeightCapacity, locationListFromFile);
                if (location != null)
                {
                    locationList.Add(location);
                    currentWeightCapacity -= location.PackageWeight;
                    locationListFromFile.Remove(location);
                }
            }

            trip.Locations = locationList;

            Assert.NotEqual(maximumWeight, trip.Locations.Sum(x => x.PackageWeight));
        }

        [Theory]
        [InlineData(300)]
        [InlineData(250)]
        [InlineData(200)]
        [InlineData(170)]
        public void MakeTripItineraryTest(int weight)
        {
            int id = 1;
            var drone = new Drone
            {
                Order = 1,
                Name = "Drone Test",
                MaximumWeight = weight
            };
            var listString = ListStringFile1();
            var locationListFromFile = _processFileService.CreateListLocation(listString);

            var trip = _tripService.MakeTripItinerary(id, drone, locationListFromFile);

            Assert.Equal(trip.Drone.MaximumWeight, trip.Locations.Sum(x => x.PackageWeight));
        }

        [Fact]
        public void MakeTripForAllDronesTest()
        {
            var listString = ListStringFile1();
            var droneList = _processFileService.CreateListDrone(listString);
            var locationList = _processFileService.CreateListLocation(listString);
            var locationListToDecrease = _processFileService.CreateListLocation(listString);
            var tripList = _tripService.MakeTripForAllDrones(droneList, locationListToDecrease);

            var tripLocationList = new List<Location>();
            foreach (var trip in tripList)
            {
                tripLocationList.AddRange(trip.Locations);
            }

            Assert.Equal(locationList.Count, tripLocationList.Count);
        }
    }
}
