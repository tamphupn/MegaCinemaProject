using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IFilmFormatRepository : IRepository<FilmFormat>
    {
        //triển khai phương thức mới không phải CRUD + paging
    }

    public class FilmFormatRepository:RepositoryBase<FilmFormat>, IFilmFormatRepository
    {
        public FilmFormatRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
}
