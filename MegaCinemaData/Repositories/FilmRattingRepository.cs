using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IFilmRattingRepository : IRepository<FilmRating>
    {
        //Thêm method khác (các method cơ bản đã hỗ trợ)
    }
    public class FilmRattingRepository : RepositoryBase<FilmRating>, IFilmRattingRepository
    {
        public FilmRattingRepository(IDbFactory dbfactory) : base(dbfactory)
        {

        }
    }
}
