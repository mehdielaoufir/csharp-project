namespace CLI.Thera.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Race { get; set; }
        public string? Gender { get; set; }

        public string? Diagnoses { get; set; }
        public string? Prescriptions { get; set; }

        public override string ToString()
        {
            return $"{Name} | {Gender}, {Race} | DOB: {BirthDate.ToShortDateString()} | Address: {Address}";
        }
    }
}
