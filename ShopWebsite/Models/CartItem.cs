namespace ShopWebsite.Models
{
    public class CartItem
    {
        public int CarId { get; set; }
        public string CarModel { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Quantity * Price; } }
        public string Image { get; set; }

        public CartItem(Car car)
        {
            CarId = car.Id;
            CarModel = car.Model;
            Price = car.Price;
            Quantity = 1;
            Image = car.Image;
        }

    }
}
