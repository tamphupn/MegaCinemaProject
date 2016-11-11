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
    [Table("BookingTickets")]
    public class BookingTicket:Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingTicketID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3), DefaultValue("MEA")]
        public string BookingTicketPrefix { get; set; }

        [DataType("nvarchar"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string BookingTicketCode { get; set; }

        [Required]
        public int BookingTicketFilmID { get; set; }

        [Required]
        public int BookingTicketRoomID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string BookingTicketTimeDetail { get; set; }

        [Required]
        public decimal BookingTicketPrice { get; set; }

        public DateTime BookingPaymentDate { get; set; }

        [Required]
        public int? CustomerID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string BookingTicketStatusID { get; set; }

        [ForeignKey("BookingTicketFilmID")]
        public virtual Film Film { get; set; }

        [ForeignKey("BookingTicketRoomID")]
        public virtual RoomFilm RoomFilm { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("BookingTicketStatusID")]
        public virtual Status Status { get; set; }

        public virtual ICollection<TicketDetail> TicketDetails { get; set; }
        public virtual ICollection<TicketCombo> TicketCombos { get; set; }
    }
}
