using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class DoctorDto
    {
        public string Bio { get; set; }
        public int UserId { get; set; }
        public int SpecialtyId { get; set; }
    }
}