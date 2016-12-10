using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IPromotionRepository: IRepository<Promotion>
    {
        //triển khai phương thức mới không phải CRUD + paging
    }
    public class PromotionRepository: RepositoryBase<Promotion>,IPromotionRepository
    {
        public PromotionRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
}
