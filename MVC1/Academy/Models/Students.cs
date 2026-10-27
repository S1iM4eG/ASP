using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class Students
    {
        [Key]
        public int stud_id { get; set; }
        [Required]
        [DisplayName("Фамилия")]
        public string last_name { get; set; }
        [Required]
        [DisplayName("Имя")]
        public string first_name { get; set; }
        [DisplayName("Отчество")]
        public string? middle_name { get; set; }
        
        [DisplayName("Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime birth_date { get; set; }
        [DisplayName("Email")]
        public string? email { get; set; }
        [DisplayName("Телефон")]
        public string? phone { get; set; }
        //public byte[]? photo { get; set; }
        [DisplayName("Группа")]
        public int? group { get; set; }
        
        //Navigation properties:

    }
}
