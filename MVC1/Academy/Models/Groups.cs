using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Academy.Models
{
    public class Groups
    {
        [Key]
        public int group_id { get; set; }
        [Required]
        [StringLength(10)]
        [DisplayName("Группа")]
        public string group_name { get; set; }
        [Required]
        [DisplayName("Направление")]
        public byte direction { get; set; }
        [DisplayName("Дата начала")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? start_date { get; set; }
        [DisplayName("Время начала")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? start_time { get; set; }
        [DisplayName("Дней обучения")]
        public byte? learning_days { get; set; }

    }
}
