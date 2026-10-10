using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Enrollment
    {
        public enum Grade { A,B,C,D,F }
        public class Enrollment
        {
            public int EnrollmentID { get; set; }
            public int CoursID { get; set; }
            public int StudentID { get; set; }
            public Grade? Grade { get; set; }

            // Navigation properties:
            public Student Student { get; set; }
            public Course Course { get; set; }
        }
    }
}
