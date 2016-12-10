using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IDetailFormatRepository : IRepository<DetailFormat>
    {
        //triển khai phương thức mới không phải CRUD + paging
    }
    public class DetailFormatRepository:RepositoryBase<DetailFormat>,IDetailFormatRepository
    {
        public DetailFormatRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
}
