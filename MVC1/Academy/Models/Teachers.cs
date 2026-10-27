using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class Teachers
    {
        [Key]
        public short teacher_id { get; set; }
        [DisplayName("Фамилия")]
        [Required]
        [StringLength(50)]
        public string? last_name { get; set; }
        [DisplayName("Имя")]
        [Required]
        [StringLength(50)]
        public string? first_name { get; set; }
        [DisplayName("Отчество")]
        [StringLength(50)]
        public string? middle_name { get; set; }
        [DisplayName("Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? birth_date { get; set; }
        [DisplayName("Email")]
        public string? email { get; set; }
        [DisplayName("Телефон")]
        public string? phone { get; set; }
        //[DisplayName("Фото")]
        //public byte[]? photo { get; set; }
        [DisplayName("Дата начала работы")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? work_since { get; set; }
        [DisplayName("Рейтинг")]
        public decimal? rate { get; set; }
    }
}
