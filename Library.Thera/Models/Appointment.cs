using System;

namespace CLI.Thera.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string Doctor { get; set; } = "";
        public string Patient { get; set; } = "";
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return $"Doctor: {Doctor}, Patient: {Patient}, Time: {Time}";
        }
    }
}
