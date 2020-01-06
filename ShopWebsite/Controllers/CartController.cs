using Microsoft.AspNetCore.Mvc;
using ShopWebsite.Infrastructure;
using ShopWebsite.Models;
using ShopWebsite.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };
            return View(cartVM);
        }

        public async Task<IActionResult> Add(int id)
        {
            Car car = await _context.Cars.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(x => x.CarId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(car));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);
            if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return RedirectToAction("Index");

            return ViewComponent("SmallCart");
        }

        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(x => x.CarId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
            }
            else
            {
                cart.RemoveAll(x => x.CarId == id);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(x => x.CarId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            return RedirectToAction("Index");

        }

        public IActionResult Clear()
        {

            HttpContext.Session.Remove("Cart");


            return Redirect(Request.Headers["Referer"].ToString());

        }
    }
}