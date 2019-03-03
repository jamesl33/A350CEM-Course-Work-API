using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A350CEM_Course_Work.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeNumber { get; set; }

        public virtual Team Team { get; set; }
    }
}