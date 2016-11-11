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
    [Table("TicketCategories")]
    public class TicketCategory:Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketCateID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string TicketCateName { get; set; }

        [Required]
        public decimal TicketCatePrice { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string TicketCateStatusID { get; set; }

        [ForeignKey("TicketCateStatusID")]
        public virtual Status Status { get; set; }

        public virtual ICollection<TicketDetail> TicketDetails { get; set; }
    }
}
