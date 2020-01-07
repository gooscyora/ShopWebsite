using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Infrastructure;
using ShopWebsite.Models;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin, editor")]
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
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;

                var slug = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The title already exists");
                    return View(page);
                }

                _context.Add(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The page has been created";

                return RedirectToAction("Index");
            }

            return View(page);
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

        public async Task<IActionResult> Edit(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");

                var slug = await _context.Pages.Where(x => x.Id != page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "The page already exists.");
                    return View(page);
                }

                _context.Update(page);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The page has been edited";

                return RedirectToAction("Edit");
            }
            return View(page);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Page page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                TempData["Error"] = "The page does not exist";
            }
            else
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "The page has been deleted";

            return RedirectToAction("Index");
        }
    }
}