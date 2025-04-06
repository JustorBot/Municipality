using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EvironmentalMunicipality.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("Home/Error400")]
        public IActionResult Error400()
        {
            _logger.LogWarning("HTTP 400 Bad Request encountered");
            ViewBag.ErrorMessage = "The request was invalid. Please check your input and try again.";
            return View("Error");
        }

        [Route("Home/Error{code:int}")]
        public IActionResult Error(int code)
        {
            ViewBag.ErrorCode = code;
            return View("Error");
        }
    }
}