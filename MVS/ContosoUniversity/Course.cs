using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Эта анотация отключает автоинкремент
        public int CoursID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        //Navigation properties:
       public ICollection<Enrollment> Enrollments { get; set; }

    }
}
