using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Api.Models
{
     [Table("Sizes")]
    public class Size
    {
         public int Id { get; set; }
        public string Value { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public byte Quantity { get; set; }
        public bool IsSelected { get; set; }

    }
}