using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaData.Repositories
{
    public interface ICinemaFeatureRepository: IRepository<CinemaFeature>
    {

    }

 

   public class CinemaFeatureRepository: RepositoryBase<CinemaFeature>, ICinemaFeatureRepository
    {
        public CinemaFeatureRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
    
}
