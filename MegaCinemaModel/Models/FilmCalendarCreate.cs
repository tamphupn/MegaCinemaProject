using MegaCinemaModel.Abstracts;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MegaCinemaModel.Models
{
    [Table("FilmCalendarCreates")]
    public class FilmCalendarCreate:Auditable
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilmCalendarCreateID { get; set; }

        [Required]
        public int FilmSessionID { get; set; }

        [Required]
        public int StaffID { get; set; }

        [Required, DataType("nvarchar")]
        public string FilmCalendarContent { get; set; }

        [DataType("nvarchar"),MaxLength(100)]
        public string FilmCalendarDescription { get; set; }
        
        [Required,DataType("nvarchar"),MaxLength(3)]
        public string StatusID { get; set; }
        
        [ForeignKey("FilmSessionID")]
        public virtual FilmSession FilmSession { get; set; }

        [ForeignKey("StaffID")]
        public virtual Staff Staff { get; set; }

        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }
    }
}
