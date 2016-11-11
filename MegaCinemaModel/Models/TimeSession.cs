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
    [Table("TimeSessions")]
    public class TimeSession:Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimeSessionID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string TimeDetail { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string TimeStatus { get; set; }

        [ForeignKey("TimeStatus")]
        public virtual Status Status { get; set; }
    }
}
