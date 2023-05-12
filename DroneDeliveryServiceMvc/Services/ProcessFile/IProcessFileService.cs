using DroneDeliveryServiceMvc.Models;

namespace DroneDeliveryServiceMvc.Services.ProcessFile
{
    public interface IProcessFileService
    {
        List<string> ReadAsString(IFormFile file);
        List<Drone> CreateListDrone(List<string> listFile);
        List<Location> CreateListLocation(List<string> listFile);
    }
}
