using System.ComponentModel.DataAnnotations;

namespace Movies.Models
{
    public class Movie
    {
        public int ID { get; set; }
        
        [Required] /*[] - Анотации*/
        //[StringLength(50, MinimumLength=2)]
        [MinLength(2, ErrorMessage = "Слишком мало") , MaxLength(50, ErrorMessage ="Слишком много")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [RangeAttribute(typeof(DateOnly), "1888-10-14", "2128-12-31", ErrorMessage ="Тогда кино еще не снимали, либо уже ничего не снимут")]
        [Display(Name = "Дата выхода")]
        public DateOnly RealeaseDate { get; set; }
        public string? Genre { get; set; }
        [Display(Name = "Жанр")]
        public string? URL { get; set; }
        public string Poster { get; set; }
    }
}
