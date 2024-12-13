namespace Models.Dtos
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string Name { get; set; }

        public Specialty Specialty { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
