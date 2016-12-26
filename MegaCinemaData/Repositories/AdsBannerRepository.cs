using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IAdsBannerRepository : IRepository<AdsBanner>
    {

    }

    public class AdsBannerRepository:RepositoryBase<AdsBanner>, IAdsBannerRepository
    {
        public AdsBannerRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
}
