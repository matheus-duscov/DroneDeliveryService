using DroneDeliveryServiceMvc.Models;
using System.Text;

namespace DroneDeliveryServiceMvc.Services.ProcessFile
{
    public class ProcessFileService : IProcessFileService
    {
        public List<string> ReadAsString(IFormFile file)
        {
            List<string> listFile = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    listFile.Add(reader.ReadLine());
                }
            }
            return listFile;
        }

        public List<Drone> CreateListDrone(List<string> listFile)
        {
            var droneSpecsArray = listFile[0].Replace(" ","").Split(",");
            var listDrone = new List<Drone>();
            int order = 1;

            for (int i = 0; i < droneSpecsArray.Length; i += 2)
            {
                listDrone.Add(new Drone
                {
                    Name = droneSpecsArray[i],
                    StringMaximumWeight = droneSpecsArray[i + 1],
                    MaximumWeight = Convert.ToInt32(droneSpecsArray[i + 1].Replace("[", "").Replace("]", "")),
                    Order = order
                });

                order++;
            }

            return listDrone;
        }

        public List<Location> CreateListLocation(List<string> listFile) 
        { 
            var listLocation = new List<Location>();

            for (int i = 1; i < listFile.Count; i++)
            {
                var locationSpecsArray = listFile[i].Replace(" ", "").Split(",");
                listLocation.Add(new Location
                {
                    Name = locationSpecsArray[0],
                    StringPackageWeight = locationSpecsArray[1],
                    PackageWeight = Convert.ToInt32(locationSpecsArray[1].Replace("[", "").Replace("]", ""))
                });
            }

            return listLocation;
        }
    }
}
