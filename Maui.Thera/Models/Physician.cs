namespace Maui.Thera.Models
{
    public class Physician
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Specialty { get; set; } = "";
        public string Phone { get; set; } = "";

        public string Name => $"{FirstName} {LastName}";
    }
}
