using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMVC.Controllers
{
	public class ArticleController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
