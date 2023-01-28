using Microsoft.AspNetCore.Mvc;
using OutloudTeskTask.Models;
using System.Diagnostics;

namespace OutloudTeskTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}