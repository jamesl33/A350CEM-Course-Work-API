using System.ComponentModel.DataAnnotations;

namespace A350CEM_Course_Work.Models
{
    public class Aircraft
    {
        public int Id { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Milage { get; set; }

        [Required]
        public int FlightHours { get; set; }
    }
}