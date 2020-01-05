using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Infrastructure;
using ShopWebsite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Controllers
{
    public class PagesController : Controller
    {
        private readonly ShopDbContext _context;

        public PagesController(ShopDbContext _context)
        {
            this._context = _context;
        }

        //GET / or /slug
        public async Task<IActionResult> Page(string slug)
        {
            if (slug == null)
            {
                return View(await _context.Pages.Where(x => x.Slug == "home").FirstOrDefaultAsync());
            }

            Page page = await _context.Pages.Where(x => x.Slug == slug).FirstOrDefaultAsync();

            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }
    }
}
