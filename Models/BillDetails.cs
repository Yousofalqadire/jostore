using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class BillDetails
    {
         public int Id { get; set; }
        public int BillId { get; set; }
        public int ProductId { get; set; }
        public string ProductBrand{ get; set; }
        public string SelectedSize { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}