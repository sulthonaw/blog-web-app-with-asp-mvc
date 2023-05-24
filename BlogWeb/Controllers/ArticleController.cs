using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogWeb.Models;


namespace BlogWeb.Controllers
{
	public class ArticleController : Controller
	{
		private readonly BloggingContext _context;

		private readonly IWebHostEnvironment _hostingEnvirontment;

		public ArticleController(BloggingContext context, IWebHostEnvironment hostingEnvirontment)
		{
			_context = context;
			_hostingEnvirontment = hostingEnvirontment;
		}

		// GET: Article
		public async Task<IActionResult> Index()
		{
			return _context.Articles != null ?
						View(await _context.Articles.ToListAsync()) :
						Problem("Entity set 'BloggingContext.Articles'  is null.");
		}

		// GET: Article/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Articles == null)
			{
				return NotFound();
			}

			var articleModel = await _context.Articles
				.FirstOrDefaultAsync(m => m.Id == id);
			if (articleModel == null)
			{
				return NotFound();
			}

			return View(articleModel);
		}

		// GET: Article/Create
		public IActionResult Create()
		{
			return View();
		}


		// POST: Article/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Description,Excerpt,Author,TimeRead,Category")] ArticleModel articleModel, IFormFile? FeaturedImg)
		{
			articleModel.PublishDate = DateTime.Now;
			if (FeaturedImg != null)
			{
				string nameFile = $"__{DateTime.Now.Day}_{DateTime.Now.Year}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{articleModel.Author}";
				string folderPath = Path.Combine(_hostingEnvirontment.WebRootPath, "img");
				string filePath = Path.Combine(folderPath, nameFile);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await FeaturedImg.CopyToAsync(stream);
				}

				articleModel.FeaturedImg = nameFile;
			}
			if (ModelState.IsValid)
			{
				_context.Add(articleModel);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(articleModel);
		}

		// GET: Article/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Articles == null)
			{
				return NotFound();
			}

			var articleModel = await _context.Articles.FindAsync(id);
			if (articleModel == null)
			{
				return NotFound();
			}
			return View(articleModel);
		}

		// POST: Article/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Excerpt,Author,TimeRead,Category")] ArticleModel articleModel, IFormFile? FeaturedImg)
		{
			articleModel.PublishDate = DateTime.Now;

			if (FeaturedImg != null)
			{
				string gambarLama = Path.Combine(_hostingEnvirontment.WebRootPath, "img");
				if (System.IO.File.Exists(gambarLama))
				{
					System.IO.File.Delete(gambarLama);
				}


				string nameFile = $"__{DateTime.Now.Day}_{DateTime.Now.Year}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{articleModel.Author}";
				string folderPath = Path.Combine(_hostingEnvirontment.WebRootPath, "img");
				string filePath = Path.Combine(folderPath, nameFile);
				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await FeaturedImg.CopyToAsync(stream);
				}

				articleModel.FeaturedImg = nameFile;
			}

			if (id != articleModel.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(articleModel);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ArticleModelExists(articleModel.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(articleModel);
		}

		// GET: Article/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Articles == null)
			{
				return NotFound();
			}

			var articleModel = await _context.Articles
				.FirstOrDefaultAsync(m => m.Id == id);
			if (articleModel == null)
			{
				return NotFound();
			}

			return View(articleModel);
		}

		// POST: Article/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Articles == null)
			{
				return Problem("Entity set 'BloggingContext.Articles'  is null.");
			}
			var articleModel = await _context.Articles.FindAsync(id);
			if (articleModel != null)
			{
				_context.Articles.Remove(articleModel);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ArticleModelExists(int id)
		{
			return (_context.Articles?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
