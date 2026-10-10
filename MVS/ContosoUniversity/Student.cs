using ContosoUniversity.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        DateTime EnrollmentDate { get; set; }

        //Navigation properties:
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
