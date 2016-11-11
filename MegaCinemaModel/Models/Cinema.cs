using MegaCinemaModel.Abstracts;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCinemaModel.Models
{
    [Table("Cinemas")]
    public class Cinema:Auditable
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CinemaID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3), DefaultValue("CNM")]
        public string CinemaPrefix { get; set; }

        [DataType("nvarchar"), MaxLength(100), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CinemaCode { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string CinemaFullName { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string CinemaAddress { get; set; }

        [Required, DataType("varchar"), MaxLength(15)]
        public string CinemaPhone { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string CinemaEmail { get; set; }

        [Required]
        public int CinemaManagerID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string CinemaStatus { get; set; }

        [ForeignKey("CinemaManagerID")]
        public virtual Staff Staff { get; set; }

        [ForeignKey("CinemaStatus")]
        public virtual Status Status { get; set; }

        public virtual ICollection<FeatureDetail> FeatureDetails { get; set; }
        public virtual ICollection<RoomFilm> RoomFilms { get; set; }
        public virtual ICollection<FilmSession> FilmSessions { get; set; }
        public virtual ICollection<PromotionCine> PromotionCines { get; set; }
    }
}
