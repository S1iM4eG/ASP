using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity1.Models
{
    public class Student
    {
        public int ID{ get;set; } 
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Navigation properties:
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
