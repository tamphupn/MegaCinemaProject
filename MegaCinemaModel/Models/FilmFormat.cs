using MegaCinemaModel.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCinemaModel.Models
{
    [Table("FilmFormats")]
    public class FilmFormat : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilmFormatID { get; set; }

        [Required]
        [DataType("nvarchar"), MaxLength(100)]
        public string FilmFormatName { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string FilmFormatDescrip { get; set; }

        public virtual ICollection<DetailFormat> DetailFormats { get; set; }
    }
}