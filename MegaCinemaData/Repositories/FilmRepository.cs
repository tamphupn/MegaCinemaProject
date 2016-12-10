using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaData.Repositories
{
    public interface IFilmRepository : IRepository<Film>
    {
        //Thêm method khác (các method cơ bản đã hỗ trợ)
    }
    public class FilmRepository : RepositoryBase<Film>, IFilmRepository
    {
        public FilmRepository(IDbFactory dbfactory) : base(dbfactory)
        {

        }
    }
}
