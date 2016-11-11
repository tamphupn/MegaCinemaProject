using MegaCinemaModel.Abstracts;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCinemaModel.Models
{
    [Table("Staffs")]
    public class Staff : Auditable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3), DefaultValue("STF")]
        public string StaffPrefix { get; set; }

        [DataType("nvarchar"), MaxLength(100), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string StaffCode { get; set; }

        [Required]
        public int StaffRegencyID { get; set; }

        [Required, DataType("nvarchar"), MaxLength(3)]
        public string StaffStatus { get; set; }

        [ForeignKey("StaffRegencyID")]
        public virtual Regency Regency { get; set; }

        [ForeignKey("StaffStatus")]
        public virtual Status Status { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; }
    }
}