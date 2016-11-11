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
    [Table("SeatMaintenances")]
    public class SeatMaintenance:Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int SeatID { get; set; }

        [Required]
        public int RoomID { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string Description { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string SeatStatusID { get; set; }

        [ForeignKey("SeatStatusID")]
        public virtual Status Status { get; set; }

        [ForeignKey("SeatID")]
        public virtual SeatList SeatList { get; set; }

        [ForeignKey("RoomID")]
        public virtual RoomFilm RoomFilm { get; set; }
    }
}
