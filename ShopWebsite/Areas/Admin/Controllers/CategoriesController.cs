using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Infrastructure;
using ShopWebsite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ShopDbContext _context;

        public CategoriesController(ShopDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.OrderBy(x => x.Sorting).ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            var slug = category.Name.ToLower().Replace(" ", "-");

            var checkDuplicate = await _context.Categories.FirstOrDefaultAsync(x => x.Slug == slug);
            if (ModelState.IsValid)
            {
                if (checkDuplicate == null)
                {
                    category.Sorting = 100;
                    category.Slug = slug;
                    await _context.AddAsync(category);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "The category has been added";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "This category already exists. Please enter a different one.");
                }
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Categories.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The category already exists.");
                    return View(category);
                }

                _context.Update(category);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The category has been edited!";

                return RedirectToAction("Edit");
            }
            return View(category);
        }
    }
}