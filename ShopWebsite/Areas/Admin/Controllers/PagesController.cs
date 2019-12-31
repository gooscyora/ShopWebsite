using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Infrastructure;
using ShopWebsite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {

        private readonly ShopDbContext _context;

        public PagesController(ShopDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var pagesList = await _context.Pages.OrderBy(x => x.Sorting).ToListAsync<Page>();

            return View(pagesList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {
                var slug = page.Title.Replace(" ", "-").ToLower();

                var checkSlug = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == slug);

                if (checkSlug == null)
                {
                    page.Slug = slug;
                    await _context.AddAsync(page);
                }
                else
                {
                    return View();
                }
                await _context.SaveChangesAsync();
            }

            return View();
        }


        public async Task<IActionResult> Details(int id)
        {
            Page page = await _context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }
        public IActionResult Create() => View();

        public async Task<IActionResult> Delete(int id)
        {

        }

    }
}