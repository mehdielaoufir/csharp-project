using System;

namespace CLI.Thera.Models
{
    public class Appointment
    {
        public Physician? Doctor { get; set; }
        public Patient? Patient { get; set; }
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return $"Doctor: {Doctor!.Name}, Patient: {Patient!.Name}, Time: {Time}";
        }
    }
}

