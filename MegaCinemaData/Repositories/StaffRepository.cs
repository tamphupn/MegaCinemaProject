using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaData.Repositories
{
   

    public interface IStaffRepository : IRepository<Staff>
    {

    }

    public class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(IDbFactory dbfactory) : base(dbfactory)
        {

        }
    }
}
