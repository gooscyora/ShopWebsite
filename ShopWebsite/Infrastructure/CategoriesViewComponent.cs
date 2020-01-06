

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopWebsite.Infrastructure
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ShopDbContext _context;

        public CategoriesViewComponent(ShopDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await GetPagesAsync();
            return View(pages);
        }

        private Task<List<CarType>> GetPagesAsync()
        {
            return _context.CarTypes.ToListAsync();
        }


    }
}
