using System;

namespace Library.Thera
{
    public class Appointment
    {
        public string? Doctor { get; set; }
        public string? Patient { get; set; }
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return $"Doctor: {Doctor}, Patient: {Patient}, Time: {Time}";
        }
    }
}
