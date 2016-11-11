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
    [Table("Promotions")]
    public class Promotion:Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromotionID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string PromotionHeader { get; set; }

        [Required, DataType("nvarchar")]
        public string PromotionContent { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string PromotionPoster { get; set; }

        [Required]
        public DateTime PromotionDateFinish { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string PromotionStatusID { get; set; }

        [ForeignKey("PromotionStatusID")]
        public virtual Status Status { get; set; }

        public virtual ICollection<PromotionCine> PromotionCines { get; set; }
    }
}
