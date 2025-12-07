using System;

namespace Maui.Thera.Models
{
    public class Physician
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string LicenseNumber { get; set; } = "";
        public DateTime GraduationDate { get; set; } = DateTime.Today.AddYears(-4);
        public string Specialization { get; set; } = "";   // e.g. "Cardiology"
    }
}
