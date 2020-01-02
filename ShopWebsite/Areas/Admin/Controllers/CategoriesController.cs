using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Infrastructure;

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

        public IActionResult Index() => View();
    }
}