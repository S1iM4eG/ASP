using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity1.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Эта аннотация отключает автоинкремент
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        // Navigation properties:
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
