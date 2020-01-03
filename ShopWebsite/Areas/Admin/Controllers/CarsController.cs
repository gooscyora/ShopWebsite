using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Infrastructure;

namespace ShopWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarsController
    {
        private readonly ShopDbContext _context;

        public CarsController(ShopDbContext _context)
        {
            this._context = _context;
        }

        public IActionResult Index() => Index();
    }
}
