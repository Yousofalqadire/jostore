using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class ProductDto
    {
        [Required]
        public String Brand { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public String SupCategory { get; set; }

        [Required]
        public double Price { get; set; }

        public double SalePrice { get; set; }
        [Required]
        public bool IsPupular { get; set; }

        public string Description { get; set; }

        public string AddedDate { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; }
        public List<string> Sizes { get; set; }
    }
}