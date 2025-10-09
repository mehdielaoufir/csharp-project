using System;

namespace CLI.Thera.Models
{
    public class Physician
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime GraduationDate { get; set; }
        public string? Specialization { get; set; }   // example: "Cardiology"

        public override string ToString()
        {
            return $"{Name} | License: {LicenseNumber} | Grad: {GraduationDate.ToShortDateString()} | {Specialization}";
        }
    }
}
