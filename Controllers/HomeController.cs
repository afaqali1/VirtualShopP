using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskJuly24.Models;
using VirtualShop.Models;

namespace TaskJuly24.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult ClientView()
        {
            return View();
        }

        public IActionResult Index()
        {
            //var userName = HttpContext.Session.GetString("UN");
            //if(string.IsNullOrEmpty(userName)) return RedirectToAction("Index", "Login");
            return View();
        }

        public IActionResult AdminView()
        {
            return View();
        }

        public IActionResult Cart(List<Cart> carts)
        {
            return View(carts);
        }
        [HttpPost]
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}