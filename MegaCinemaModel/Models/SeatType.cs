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
    [Table("SeatTypes")]
    public class SeatType:Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatTypeID { get; set; }

        [Required, DataType("nvarchar"),MaxLength(100)]
        public string SeatTypeName { get; set; }

        [Required]
        public decimal SeatTypeSurcharge { get; set; }

        [Required,DataType("nvarchar"),MaxLength(3)]
        public string SeatTypeStatus { get; set; }
        
        [ForeignKey("SeatTypeStatus")]
        public virtual Status Status { get; set; }

        public virtual ICollection<TicketDetail> TicketDetails { get; set; }
    }
}
