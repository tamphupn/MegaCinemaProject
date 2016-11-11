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
    [Table("CinemaFeatures")]
    public class CinemaFeature:Auditable
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeatureID { get; set; }

        [Required,DefaultValue(true)] //true: content - false: image
        public bool FeatureType { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string FeatureContent { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string FeatureDescription { get; set; }

        public virtual ICollection<FeatureDetail> FeatureDetails { get; set; }
    }
}
