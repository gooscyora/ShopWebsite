using ShopWebsite.Models;
using System.Collections.Generic;

namespace ShopWebsite.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
