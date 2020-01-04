using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopWebsite.Infrastructure;
using ShopWebsite.Models;
using ShopWebsite.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarsController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CarsController(ShopDbContext _context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = _context;
            this.webHostEnvironment = webHostEnvironment;
        }

        //GET admin/cars
        [Route("")]
        [Route("Admin/Cars/{page?}")]
        public async Task<IActionResult> Index(int page = 1)
        {
            PagingViewModel cars = new PagingViewModel()
            {
                PagingHelper = new PagingHelper
                {
                    ItemsPerPage = 6,
                    CurrentPage = page,
                    TotalItems = await _context.Cars.CountAsync()
                }
            };
            cars._contextPaging = await _context.Cars.OrderBy(x => x.Id).Include(x => x.CarType)
             .Skip((page * cars.PagingHelper.ItemsPerPage) - cars.PagingHelper.ItemsPerPage)
             .Take(cars.PagingHelper.ItemsPerPage).ToListAsync();
            return View(cars);
        }

        //GET admin/cars/create
        public IActionResult Create()
        {
            ViewBag.CarTypeId = new SelectList(_context.CarTypes.OrderBy(x => x.Sorting), "Id", "Name");

            return View();
        }



        //POST admin/cars/create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {

                string imageName = "defaultimage.jpg";

                if (car.ImageUpload != null)
                {

                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "static/cars");

                    //GUID is generating unique ID
                    imageName = Guid.NewGuid().ToString() + "_" + car.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    await car.ImageUpload.CopyToAsync(fileStream);
                    fileStream.Close();
                }

                car.Image = imageName;

                _context.Add(car);
                await _context.SaveChangesAsync();

                TempData["Success"] = "The car has been created";

                return RedirectToAction("Index");
            }
            ViewBag.CarTypeId = new SelectList(_context.CarTypes.OrderBy(x => x.Sorting), "Id", "Name");
            return View(car);
        }


        //GET admin/cars/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            Car car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                TempData["Error"] = "Car does not exist";
            }
            else
            {
                if (!string.Equals(car.Image, "noimage.png"))
                {

                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "static/cars");

                    string oldImagePath = Path.Combine(uploadsDir, car.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);

                    }
                }
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Car has been deleted";
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Details(int id)
        {
            Car car = await _context.Cars.Include(c => c.CarType).FirstOrDefaultAsync(x => x.Id == id);
            if (car == null)
            {

                return NotFound();
            }

            return View(car);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                TempData["Error"] = "This car does not exist";
                return RedirectToAction("Index");
            }

            ViewBag.CarTypeId = new SelectList(_context.CarTypes.OrderBy(x => x.Sorting), "Id", "Name");

            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            if (ModelState.IsValid)
            {
                if (car.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(webHostEnvironment.WebRootPath, "static/cars");

                    if (!string.Equals(car.Image, "noimage.png"))
                    {
                        string oldImagePath = Path.Combine(uploadsDir, car.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);

                        }
                    }

                    //GUID is giving unique ID
                    string imageName = Guid.NewGuid().ToString() + "_" + car.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);
                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await car.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                    car.Image = imageName;
                }

                _context.Update(car);

                await _context.SaveChangesAsync();

                TempData["Success"] = "Car has been successfully edited";

                return RedirectToAction("Index");
            }
            ViewBag.CarTypeId = new SelectList(_context.CarTypes.OrderBy(x => x.Sorting), "Id", "Name");
            return View(car);
        }
    }

}

