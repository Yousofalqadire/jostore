using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
namespace Api.Dtos
{
    public class UpdateProductDto
    {
     
     public int Id { get; set; }
        public String Brand { get; set; }
        
        public string Category { get; set; }
        
        public String SupCategory { get; set; }

       
        public double Price { get; set; }

        public double SalePrice { get; set; }
       
        public bool IsPupular { get; set; }

        public string Description { get; set; }
       
        public List<IFormFile> Photos { get; set; }
        public List<string> Sizes { get; set; }
    }
}