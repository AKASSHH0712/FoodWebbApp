using Food.Repository;
using FoodWebbApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodWebbApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdmin _admin;

        public HomeController(IAdmin admin)
        {
            _admin = admin;
        }
       
        public IActionResult Index()
        {
           
                var obj = _admin.GetProducts();
                return View(obj);
         
            
        }
      

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
