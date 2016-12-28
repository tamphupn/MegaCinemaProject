using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface ITimeSessionRepository : IRepository<TimeSession>
    {
        Film GetFilmDetail(int id);
    }
    public class TimeSessionRepository : RepositoryBase<TimeSession>, ITimeSessionRepository
    {
        public TimeSessionRepository(IDbFactory dbfactory) : base(dbfactory)
        {

        }

        public Film GetFilmDetail(int id)
        {
            return this.DbContext.FilmSessions.SingleOrDefault(n => n.FilmSessionID == id).Film;
        }
    }
}
