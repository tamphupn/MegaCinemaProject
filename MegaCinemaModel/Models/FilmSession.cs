using System;
using System.Collections.Generic;
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
    [Table("FilmSessions")]
    public class FilmSession:Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilmSessionID { get; set; }

        [Required]
        public int FilmID { get; set; }

        [Required]
        public int CinemaID { get; set; }

        [Required, DataType("nvarchar")]
        public string FilmCalendar { get; set; }

        [Required]
        public DateTime DateStartSession { get; set; }

        [Required]
        public DateTime DateFinishSession { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string FilmSessionStatusID { get; set; }

        [ForeignKey("FilmID")]
        public virtual Film Film { get; set; }

        [ForeignKey("CinemaID")]
        public virtual Cinema Cinema { get; set; }

        [ForeignKey("FilmSessionStatusID")]
        public virtual Status Status { get; set; }

    }
}
