using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Infrastructure;
using ShopWebsite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarTypesController : Controller
    {
        private readonly ShopDbContext _context;

        public CarTypesController(ShopDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CarTypes.OrderBy(x => x.Sorting).ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarType carType)
        {
            var slug = carType.Name.ToLower().Replace(" ", "-");

            var checkDuplicate = await _context.CarTypes.FirstOrDefaultAsync(x => x.Slug == slug);
            if (ModelState.IsValid)
            {
                if (checkDuplicate == null)
                {
                    carType.Sorting = 100;
                    carType.Slug = slug;
                    await _context.AddAsync(carType);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Car type has been added";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "This car type already exists. Please enter a different one.");
                }
            }

            return View(carType);
        }

        public async Task<IActionResult> Edit(int id)
        {
            CarType carType = await _context.CarTypes.FindAsync(id);
            if (carType == null)
            {
                return NotFound();
            }

            return View(carType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CarType carType)
        {
            if (ModelState.IsValid)
            {
                carType.Slug = carType.Name.ToLower().Replace(" ", "-");

                var slug = await _context.CarTypes.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Slug == carType.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "The car type already exists.");
                    return View(carType);
                }

                _context.Update(carType);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The car type has been edited!";

                return RedirectToAction("Edit");
            }
            return View(carType);
        }

        public async Task<IActionResult> Delete(int id)
        {
            CarType carType = await _context.CarTypes.FindAsync(id);

            if (carType == null)
            {
                TempData["Error"] = "The car type does not exist";
            }
            else
            {
                _context.CarTypes.Remove(carType);
                await _context.SaveChangesAsync();
                TempData["Success"] = "The car type has been deleted";
            }


            return RedirectToAction("Index");
        }
    }
}