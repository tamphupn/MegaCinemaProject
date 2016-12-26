using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaModel.Abstracts;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCinemaModel.Models
{
    [Table("AdsBanners")]
    public class AdsBanner : Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdsId { get; set; }

        public int FilmId { get; set; }

        [DataType("nvarchar")]
        public string AdsDescription { get; set; }

        [ForeignKey("FilmId")]
        public virtual Film Film { get; set; }

    }
}
