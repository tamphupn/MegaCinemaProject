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
    [Table("TicketDetails")]
    public class TicketDetail:Auditable
    {
        [Key, Column(Order = 0 )]
        public int BookingTicketID { get; set; }

        [Key, Column(Order = 1)]
        public string SeatName { get; set; }

        [Required]
        public int SeatTypeID { get; set; }

        [Required]
        public decimal SeatPrice { get; set; }

        [Required]
        public int TicketCategoryID { get; set; }

        public decimal SeatDiscount { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string TicketStatusID { get; set; }

        [ForeignKey("SeatTypeID")]
        public virtual SeatType SeatType { get; set; }

        [ForeignKey("TicketCategoryID")]
        public virtual TicketCategory TicketCategory { get; set; }

        [ForeignKey("TicketStatusID")]
        public virtual Status Status { get; set; }

        [ForeignKey("BookingTicketID")]
        public virtual BookingTicket BookingTicket { get; set; }
    }
}
