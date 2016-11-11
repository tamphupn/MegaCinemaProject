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
    [Table("RoomFilms")]
    public class RoomFilm:Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomID { get; set; }

        [Required, DataType("nvarchar"),MaxLength(3),DefaultValue("ROO")]
        public string RoomPrefix { get; set; }

        [DataType("nvarchar"),MaxLength(100),DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string RoomCode { get; set; }

        [Required, DataType("nvarchar"),MaxLength(100)]
        public string RoomName { get; set; }

        [Required, DataType("nvarchar")]
        public string RoomSeatPosition { get; set; }

        [DataType("nvarchar")]
        public string RoomCinemaDescription { get; set; }

        [DataType("nvarchar"),MaxLength(100)]
        public string RoomPoster { get; set; }

        [Required]
        public int RoomCinemaID { get; set; }

        [Required, DataType("nvarchar"),MaxLength(3)]
        public string RoomStatusID { get; set; }

        [ForeignKey("RoomCinemaID")]
        public virtual Cinema Cinema { get; set; }

        [ForeignKey("RoomStatusID")]
        public virtual Status Status { get; set; }

        public virtual ICollection<SeatMaintenance> SeatMaintenances { get; set; }

        public virtual ICollection<BookingTicket> BookingTickets { get; set; }
    }
}
