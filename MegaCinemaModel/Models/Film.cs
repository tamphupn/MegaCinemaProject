using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MegaCinemaModel.Abstracts;
using System.ComponentModel;

namespace MegaCinemaModel.Models
{
    [Table("Films")]
    public class Film:Auditable
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilmID { get; set; }

        [Required,DataType("nvarchar"),MaxLength(3),DefaultValue("FLM")]
        public string FilmPrefix { get; set; }

        [DataType("nvarchar"),MaxLength(100),DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FilmCode { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string FilmName { get; set; }

        [Required,Range(0,int.MaxValue)]
        public int FilmDuration { get; set; }

        [Required]
        public DateTime FilmFirstPremiered { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string FilmLanguage { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string FilmContent { get; set; }

        public DateTime? FilmFinishPremiered { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string FilmPoster { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string FilmCompanyRelease { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string FilmTrailer { get; set; }

        [Required]
        public int FilmRatingID { get; set; }
        
        [Required,DataType("nvarchar"),MaxLength(3)]
        public string FilmStatus { get; set; }

        [ForeignKey("FilmStatus")]
        public virtual Status Status { get; set; }

        [ForeignKey("FilmRatingID")]
        public virtual FilmRating FilmRating { get; set; }

        public virtual ICollection<DetailFormat> DetailFormats { get; set; }
        public virtual ICollection<DetailCategory> DetailCategories { get; set; }
        public virtual ICollection<FilmSession> FilmSessions { get; set; }

        public virtual ICollection<BookingTicket> BookingTickets { get; set; }
    }
}
