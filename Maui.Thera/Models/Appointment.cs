namespace Maui.Thera.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int PhysicianId { get; set; }

        public DateTime Date { get; set; }
    }
}
