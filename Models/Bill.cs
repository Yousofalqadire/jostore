using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Bill
    {
         public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte Status { get; set; }
    }
}