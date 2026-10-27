using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class Directions
    {
        [Required]
        public int direction_id { get; set; }
        [Required]
        [StringLength(50)]
        public string direction_name { get; set; }

        //Navigation properties:

    }
}
