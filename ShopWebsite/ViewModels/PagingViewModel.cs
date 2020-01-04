using ShopWebsite.Infrastructure;
using ShopWebsite.Models;
using System.Collections.Generic;

namespace ShopWebsite.ViewModels
{
    public class PagingViewModel
    {
        public IEnumerable<Car> _contextPaging { get; set; }
        public PagingHelper PagingHelper { get; set; }
    }
}
