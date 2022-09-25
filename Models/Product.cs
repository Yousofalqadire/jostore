using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String Brand { get; set; }
        public string Category { get; set; }
        public String SupCategory { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public bool IsPupular { get; set; }
        public string AddedDate { get; set; }
       public List<Photo> Photos{get; set;}
       public List<Size> Sizes { get; set; }
       public string Details { get; set; }
    }
}