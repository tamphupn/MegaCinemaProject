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
    [Table("FoodLists")]
    public class FoodList:Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3), DefaultValue("FOO")]
        public string FoodPrefix { get; set; }

        [DataType("nvarchar"), MaxLength(100), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FoodCode { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string FoodName { get; set; }

        [Required]
        public decimal FoodPrice { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string FoodDescription { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string FoodPoster { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string FoodStatus { get; set; }

        [ForeignKey("FoodStatus")]
        public virtual Status Status { get; set; }

        public virtual ICollection<TicketCombo> TicketCombos { get; set; }
    }
}
