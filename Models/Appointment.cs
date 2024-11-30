using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public User Patient { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ScheduleAt { get; set; }
    }
}