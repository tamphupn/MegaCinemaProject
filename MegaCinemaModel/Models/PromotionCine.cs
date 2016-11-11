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
    [Table("PromotionCines")]
    public class PromotionCine:Auditable
    {
        [Key,Column(Order = 0 )]
        public int PromotionID { get; set; }

        [Key, Column(Order = 1)]
        public int CinemaID { get; set; }

        public string Description { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string PromotionCineStatusID { get; set; }

        [ForeignKey("CinemaID")]
        public virtual Cinema Cinema { get; set; }

        [ForeignKey("PromotionID")]
        public virtual Promotion Promotion { get; set; }

        [ForeignKey("PromotionCineStatusID")]
        public virtual Status Status { get; set; }
    }
}
