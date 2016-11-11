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
    [Table("TicketCombos")]
    public class TicketCombo:Auditable
    {
        [Key,Column(Order = 0)]
        public int BookingTicketID { get; set; }

        [Key, Column(Order = 1)]
        public int FoodListID { get; set; }

        [Required]
        public int FoodQuantity { get; set; }

        [Required]
        public decimal FoodPrice { get; set; }

        public decimal FoodDiscount { get; set; }

        [Required]
        public decimal FoodTotalPrice { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string FoodStatusID { get; set; }

        [ForeignKey("BookingTicketID")]
        public virtual BookingTicket BookingTicket { get; set; }

        [ForeignKey("FoodListID")]
        public virtual FoodList FoodList { get; set; }

        [ForeignKey("FoodStatusID")]
        public virtual Status Status { get; set; }

    }
}
