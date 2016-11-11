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
    [Table("FeatureDetails")]
    public class FeatureDetail:Auditable
    {
        [Key,Column(Order = 0)]
        public int FeatureID { get; set; }

        [Key, Column(Order = 1)]
        public int CinemaID { get; set; }

        [DataType("nvarchar"),MaxLength(100)]
        public string Description { get; set; }

        [ForeignKey("FeatureID")]
        public virtual CinemaFeature CinemaFeature { get; set; }

        [ForeignKey("CinemaID")]
        public virtual Cinema Cinema { get; set; }


    }
}
