using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MegaCinemaModel.Abstracts;

namespace MegaCinemaModel.Models
{
    [Table("Regencies")]
    public class Regency:Auditable
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegencyID { get; set; }

        [Required,DataType("nvarchar"),MaxLength(100)]

        public string RegencyName { get; set; }

        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
