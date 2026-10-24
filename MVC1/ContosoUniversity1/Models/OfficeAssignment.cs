using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity1.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int InstructorID { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        //Navigation properties:
        public Instructor Instructor { get; set; }
    }
}