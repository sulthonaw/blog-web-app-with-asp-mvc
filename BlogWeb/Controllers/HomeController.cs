using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly BloggingContext _context;

		public HomeController(ILogger<HomeController> logger, BloggingContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var article = _context.Articles.ToList();
			return View(article);
		}

		public IActionResult Privacy()
		{
			return View();
		}
		
		public IActionResult About()
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