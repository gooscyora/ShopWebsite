using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarsController : Controller
    {
        private readonly ShopDbContext _context;

        public CarsController(ShopDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var cars = await _context.Cars.OrderByDescending(x => x.Id).Include(x => x.CarType).ToListAsync();
            return View(cars);
        }

        public IActionResult Create()
        {
            ViewBag.CarId = new SelectList(_context.CarTypes.OrderBy(x => x.Sorting), "Id", "Name");

            return View();
        }

    }
}
