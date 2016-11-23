using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaData.Repositories
{
    public interface ICinemaRepository: IRepository<Cinema>
    {
        
    }

    public class CinemaRepository: RepositoryBase<Cinema>, ICinemaRepository
    {
        public CinemaRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
   
}
