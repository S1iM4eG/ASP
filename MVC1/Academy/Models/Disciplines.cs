using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class Disciplines
    {
        [Key]
        public short discipline_id { get; set; }
        [DisplayName("Дисциплина")]
        [Required]
        [StringLength(150)]
        public string discipline_name { get; set; }
        [DisplayName("Количество занятий")]
        public byte number_of_lessons { get; set; }
    }
}
