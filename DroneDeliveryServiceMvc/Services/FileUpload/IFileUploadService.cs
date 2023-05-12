namespace DroneDeliveryServiceMvc.Services.FileUpload
{
    public interface IFileUploadService
    {
        List<Models.Trip> UploadFile(IFormFile file);
    }
}
