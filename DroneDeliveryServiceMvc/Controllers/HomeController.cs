using DroneDeliveryServiceMvc.Models;
using DroneDeliveryServiceMvc.Services.FileUpload;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DroneDeliveryServiceMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileUploadService _service;

        public HomeController(ILogger<HomeController> logger, IFileUploadService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult Index(IFormFile file)
        {
            var result = new List<Models.Trip>();

            try
            {
                result = _service.UploadFile(file);

                if (!result.Any())
                {
                    throw new Exception();
                }
            }
            catch(BusinessException ex)
            {
                ViewBag.Message = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error on processing uploaded file: " + ex;
            }

            return View(model: result);
        }
    }
}