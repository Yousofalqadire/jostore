using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class MemberDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}