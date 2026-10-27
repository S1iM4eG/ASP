using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class Directions
    {
        [Key]
        public byte direction_id { get; set; }
        [DisplayName("Направление")]
        [Required]
        [StringLength(50)]
        public string direction_name { get; set; }

        //Navigation properties:

    }
}
