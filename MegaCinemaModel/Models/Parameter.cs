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
using System.ComponentModel;

namespace MegaCinemaModel.Models
{
    [Table("Parameters")]
    public class Parameter:Auditable
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [DataType("nvarchar")]
        public string ParaName { get; set; }

        [DefaultValue(true)]
        public bool isActive { get; set; }
    }
}
