using Microsoft.AspNetCore.Mvc;
using MyMarketplase.Models;
using System.Diagnostics;

namespace MyMarketplase.Controllers
{
    public class HomeController : Controller
    {
        MyAppContext db;

        public HomeController(MyAppContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public IActionResult About()
        {
            return View();
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