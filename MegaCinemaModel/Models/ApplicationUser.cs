using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaModel.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MegaCinemaModel.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required, DataType("nvarchar"), MaxLength(100)]
        public string FirstName { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required, DefaultValue(true)] //male
        public bool Sex { get; set; }

        [Required, DataType("varchar"), MaxLength(12)]
        public string SSN { get; set; }

        [Required, DataType("nvarchar"), MaxLength(100)]
        public string Address { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string District { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string City { get; set; }

        [DataType("nvarchar"), MaxLength(100)]
        public string Avatar { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
