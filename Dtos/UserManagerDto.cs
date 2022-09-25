using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class UserManagerDto
    {
        public bool IsCreated { get; set; }
        public String Massage { get; set; }
        public string UserId { get; set; }
        public string Confirmation { get; set; }
    }
}