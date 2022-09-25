using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    public class DeletePhotoDto
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
    }
}