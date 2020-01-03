﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWebsite.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Model { get; set; }

        public string Slug { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Description { get; set; }
        public int Price { get; set; }

        public int CarTypeId { get; set; }

        public string Image { get; set; }

        [ForeignKey("CarTypeId")]
        public virtual CarType CarType { get; set; }
    }
}
