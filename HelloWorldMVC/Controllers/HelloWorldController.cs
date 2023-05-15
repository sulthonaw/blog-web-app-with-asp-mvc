using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace HelloWorldMVC.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        }

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Halo " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}