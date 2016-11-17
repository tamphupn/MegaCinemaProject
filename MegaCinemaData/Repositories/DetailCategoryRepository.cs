using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IDetailCategoryRepository : IRepository<DetailCategory>
    {
        //triển khai phương thức mới không phải CRUD + paging
    }
    public class DetailCategoryRepository:RepositoryBase<DetailCategory>, IDetailCategoryRepository
    {
        public DetailCategoryRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
}
