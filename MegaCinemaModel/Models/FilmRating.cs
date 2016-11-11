using MegaCinemaModel.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCinemaModel.Models
{
    [Table("FilmRatings")]
    public class FilmRating : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string RatingName { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string RatingDescription { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}