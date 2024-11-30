using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}