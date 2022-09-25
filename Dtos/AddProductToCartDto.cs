using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Dtos
{
    public class AddProductToCartDto
    {
        [Required]
        public Product Product { get; set;}
        public string User { get; set; }
    }
}