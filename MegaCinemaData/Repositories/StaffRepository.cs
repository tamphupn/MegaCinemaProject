using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MegaCinemaData.Repositories
{
   

    public interface IStaffRepository : IRepository<Staff>
    {
        ApplicationUser FindStaff(int i);
        ApplicationUser FindApplicationUser(string id);
    }

    public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(IDbFactory dbfactory) : base(dbfactory)
        {

        }

        public ApplicationUser FindApplicationUser(string id)
        {
            return this.DbContext.Users.SingleOrDefault(n => n.Email == id);
        }

        public ApplicationUser FindStaff(int i)
        {
            return this.DbContext.Users.SingleOrDefault(n => n.Staff.StaffID == i);
        }
    }
}
