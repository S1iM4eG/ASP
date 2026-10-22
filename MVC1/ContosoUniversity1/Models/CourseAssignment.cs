using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity1.Models
{
    [PrimaryKey(nameof(InstructorID),nameof(CourseID))]
    public class CourseAssignment
    {
        //              Composite primary key
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        //              Composite primary key

        //Navigation properties:
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
