using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaModel.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace MegaCinemaModel.Models
{
    [Table("EventTopics")]
    public class EventTopic : Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        [DataType("nvarchar")]
        public string EventTitle { get; set; }

        [DataType("nvarchar")]
        public string EventContent { get; set; }
    }
}
