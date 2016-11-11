using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IStatusRepository:IRepository<Status>
    {

    }
    public class StatusRepository:RepositoryBase<Status>, IStatusRepository
    {
        public StatusRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }
    }
}
