using MegaCinemaModel.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCinemaModel.Models
{
    [Table("FilmCategories")]
    public class FilmCategory : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilmCategoryID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string FilmCategoryName { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string FilmCategoryDescrip { get; set; }

        public virtual ICollection<DetailCategory> DetailCategories { get; set; }
    }
}