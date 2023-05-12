using DroneDeliveryServiceMvc.Services.ProcessFile;
using DroneDeliveryServiceMvc.Services.Trip;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DroneDeliveryServiceMvc.Test
{
    public class BaseTester
    {
        protected readonly IProcessFileService _processFileService;
        protected readonly ITripService _tripService;
        public BaseTester() 
        {
            var services = new ServiceCollection();
            services.AddTransient<IProcessFileService, ProcessFileService>();
            services.AddTransient<ITripService, TripService>();

            var serviceProvider = services.BuildServiceProvider();
            _processFileService = serviceProvider.GetRequiredService<IProcessFileService>();
            _tripService = serviceProvider.GetRequiredService<ITripService>();
        }

        public List<string> ListStringFile1()
        {
            string file = string.Concat(Directory.GetCurrentDirectory(), @"\input.txt");
            using var stream = new MemoryStream(File.ReadAllBytes(file).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "input", file.Split(@"\").Last());
            return _processFileService.ReadAsString(formFile);
        }
    }
}
