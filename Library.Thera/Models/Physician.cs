namespace CLI.Thera.Models
{
    public class Physician
    {
        public string? Name { get; set; }
        public string? LicenseNumber { get; set; }
        public DateTime GraduationDate { get; set; }
        public string? Specialization { get; set; }

        public override string ToString()
        {
            return $"{Name} | License: {LicenseNumber} | Grad: {GraduationDate.Year} | Specialty: {Specialization}";
        }
    }
}
