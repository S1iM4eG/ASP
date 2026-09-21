using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Academy.Models
{
    public class Group
    {
        [Key]
        public int group_id { get; set; }

        [Required]
        [StringLength(10,MinimumLength = 4)]
        [ForeignKey(nameof(Direction))]
        public string group_name { get; set; }

        [Required]
        [Column(TypeName = "TINYINT")]
        public int direction { get; set; }
        public DateOnly start_date { get; set; }
        public TimeOnly start_time { get; set; }

        [Column(TypeName = "TINYINT")]
        public int? learning_days { get; set; }

        //Navigation properties:
        public Direction Direction { get; set; }
    }
}
