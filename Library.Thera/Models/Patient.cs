namespace CLI.Thera.Models
{
    public class Patient
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Race { get; set; }
        public string? Gender { get; set; }

        public override string ToString()
        {
            return $"{Name} | {Gender}, {Race} | DOB: {BirthDate.ToShortDateString()} | Address: {Address}";
        }
    }
}
