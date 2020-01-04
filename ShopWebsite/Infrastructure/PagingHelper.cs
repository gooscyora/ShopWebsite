using System;

namespace ShopWebsite.Infrastructure
{
    public class PagingHelper
    {
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

            }
        }
    }
}
