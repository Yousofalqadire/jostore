using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class AnonymouseCart
    {
        public int  ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPric { get; set; }
        public string SelectedSize { get; set; }
        public int Quantity { get; set; }
        public string ProductPhoto { get; set; }
        public string User { get; set; }
        public double TotalPrice{get; set;}
         public BillDto BillData { get; set; }
    }
}