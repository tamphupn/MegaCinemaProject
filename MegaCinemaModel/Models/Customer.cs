using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MegaCinemaModel.Abstracts;
using System.ComponentModel;

namespace MegaCinemaModel.Models
{
    [Table("Customers")]
    public class Customer:Auditable
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }

        [Required,DataType("nvarchar"),MaxLength(3),DefaultValue("CUS")]
        public string CustomerPrefix { get; set; }

        [DataType("nvarchar"), MaxLength(100), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CustomerCode { get; set; }

        [Required, DefaultValue(0)]
        public int CustomerPoint { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string CustomerAvatar { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string CustomerIP { get; set; }

        [Required]
        public int CustomerAccountType { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string CustomerStatus { get; set; }

        [ForeignKey("CustomerAccountType")]
        public virtual AccountType AccountType { get; set; }

        [ForeignKey("CustomerStatus")]
        public virtual Status Status { get; set; }

        public virtual ICollection<BookingTicket> BookingTickets { get; set; }
    }
}
