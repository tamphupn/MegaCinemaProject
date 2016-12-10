using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IPromotionCineRepository : IRepository<PromotionCine>
    {
        //triển khai phương thức mới không phải CRUD + paging
    }

    public class PromotionCineRepository : RepositoryBase<PromotionCine>, IPromotionCineRepository
    {
        public PromotionCineRepository(IDbFactory dbfactory) : base(dbfactory)
        {

        }
    }
}
