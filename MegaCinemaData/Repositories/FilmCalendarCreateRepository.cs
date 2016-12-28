using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;
using System.Data.Entity;

namespace MegaCinemaData.Repositories
{
    public interface IFilmCalendarCreateRepository : IRepository<FilmCalendarCreate>
    {
        FilmCalendarCreate FindLastCalendarCreateFilm(int idFilm, int idCinema = 0);
        IEnumerable<FilmCalendarCreate> GetAllCalendarCreatesFilm(int idFilm);
    }
    public class FilmCalendarCreateRepository : RepositoryBase<FilmCalendarCreate>, IFilmCalendarCreateRepository
    {
        public FilmCalendarCreateRepository(IDbFactory dbfactory) : base(dbfactory)
        {
            
        }
        public FilmCalendarCreate FindLastCalendarCreateFilm(int idFilm, int idCinema = 0)
        {
            if (idCinema == 0)
            {
                var result = this.DbContext.FilmCalendarCreates.Where(n => (n.FilmSession.FilmID == idFilm && n.StatusID == "AC")).Last();
                return result;
            }
            else
            {
                var result = this.DbContext.FilmCalendarCreates.Where(n => (n.FilmSession.FilmID == idFilm && n.FilmSession.CinemaID == idCinema && n.StatusID == "AC")).Last();
                return result;
            }
        }

        public IEnumerable<FilmCalendarCreate> GetAllCalendarCreatesFilm(int idFilm)
        {
            return
                this.DbContext.FilmCalendarCreates.Include(n => n.FilmSession.Cinema)
                    .Where(n => (n.FilmSession.FilmID == idFilm && n.StatusID == "AC"));
        }
    }
}
