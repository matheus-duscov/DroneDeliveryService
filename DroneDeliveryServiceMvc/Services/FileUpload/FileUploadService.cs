using DroneDeliveryServiceMvc.Models;
using DroneDeliveryServiceMvc.Services.ProcessFile;
using DroneDeliveryServiceMvc.Services.Trip;

namespace DroneDeliveryServiceMvc.Services.FileUpload
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IProcessFileService _processFileService;
        private readonly ITripService _tripService;

        public FileUploadService(IProcessFileService processFileService, ITripService tripService)
        {
            _processFileService = processFileService;
            _tripService = tripService;
        }

        public List<Models.Trip> UploadFile(IFormFile file)
        {
            var listStringFile = _processFileService.ReadAsString(file);
            var droneList = _processFileService.CreateListDrone(listStringFile);

            if (droneList.Count > 100)
                throw new BusinessException("Error. The number of drones exceed the maximum limit of 100.");

            var locationList = _processFileService.CreateListLocation(listStringFile);
            var tripList = _tripService.MakeTripForAllDrones(droneList, locationList);

            return tripList;
        }
    }
}
