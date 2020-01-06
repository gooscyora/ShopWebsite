using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Infrastructure;

namespace ShopWebsite.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;

        public CartController(ShopDbContext _context)
        {
            this._context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}