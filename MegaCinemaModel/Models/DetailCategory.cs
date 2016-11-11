using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MegaCinemaModel.Abstracts;

namespace MegaCinemaModel.Models
{
    [Table("DetailCategories")]
    public class DetailCategory:Auditable
    {
        [Key]
        [Column(Order = 0)]
        public int FilmID { get; set; }

        [Key]
        [Column(Order = 1)]
        public int FilmCategoryID { get; set; }

        [DataType("nvarchar"),MaxLength(100)]
        public string Description { get; set; }

        [ForeignKey("FilmID")]
        public virtual Film Film { get; set; }

        [ForeignKey("FilmCategoryID")]
        public virtual FilmCategory FilmCategory { get; set; }
    }
}
