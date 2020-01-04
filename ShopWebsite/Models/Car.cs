using Microsoft.AspNetCore.Http;
using ShopWebsite.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopWebsite.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Description { get; set; }
        public int Price { get; set; }
        [Display(Name = "Car Type")]

        public int CarTypeId { get; set; }

        public string Image { get; set; }

        [ForeignKey("CarTypeId")]
        [Range(1, 100, ErrorMessage = "Car type is required.")]
        public virtual CarType CarType { get; set; }

        [FileExtension]
        [NotMapped] //Saving files locally
        public IFormFile ImageUpload { get; set; }
    }
}
