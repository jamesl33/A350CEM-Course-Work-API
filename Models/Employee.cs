using System.ComponentModel.DataAnnotations;

namespace A350CEM_Course_Work.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeNumber { get; set; }
    }
}