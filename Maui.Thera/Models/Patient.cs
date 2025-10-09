using System;

namespace Maui.Thera.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime Birthdate { get; set; } = DateTime.Today.AddYears(-18);
        public string Race { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Diagnoses { get; set; } = "";
        public string Prescriptions { get; set; } = "";
    }
}
