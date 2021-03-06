﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Infrastructure;
using ShopWebsite.Models;
using ShopWebsite.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Controllers
{
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ShopDbContext _context;

        public CarsController(ShopDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IActionResult> CarsByType(string typeSlug, int page = 1)
        {
            CarType carType = await _context.CarTypes.Where(x => x.Slug == typeSlug).FirstOrDefaultAsync();

            if (carType == null)
                return RedirectToAction("Index");

            int pageSize = 3;
            PagingViewModel cars = new PagingViewModel()
            {
                PagingHelper = new PagingHelper
                {
                    ItemsPerPage = pageSize,
                    CurrentPage = page,
                    TotalItems = await _context.Cars.Where(x => x.CarTypeId == carType.Id).CountAsync()
                }
            };
            cars.Cars = await _context.Cars.OrderBy(x => x.Id).Include(x => x.CarType)
                .Where(x => x.CarTypeId == carType.Id)
             .Skip((page * pageSize) - pageSize)
             .Take(pageSize).ToListAsync();

            return View(cars);
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 4;
            PagingViewModel cars = new PagingViewModel()
            {
                PagingHelper = new PagingHelper
                {
                    ItemsPerPage = pageSize,
                    CurrentPage = page,
                    TotalItems = await _context.Cars.CountAsync()
                }
            };
            cars.Cars = await _context.Cars.OrderBy(x => x.Id).Include(x => x.CarType)
             .Skip((page * pageSize) - pageSize)
             .Take(pageSize).ToListAsync();

            return View(cars);
        }


    }
}