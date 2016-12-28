using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IRoomFilmRepository : IRepository<RoomFilm>
    {
        
    }
    public class RoomFilmRepository: RepositoryBase<RoomFilm>, IRoomFilmRepository
    {
        public RoomFilmRepository(IDbFactory dbfactory) : base(dbfactory)
        {

        }
    }
}
