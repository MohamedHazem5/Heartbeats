using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class DoctorDetailsDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string Bio { get; set; }
        public string SpecialtyName { get; set; }
    }
}