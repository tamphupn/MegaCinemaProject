using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;
using MegaCinemaModel.Models;

namespace MegaCinemaService
{
    public interface IFilmCalendarCreateService
    {
        FilmCalendarCreate FindLastCalendarCreateFilm(int idFilm, int idCinema);
        IEnumerable<FilmCalendarCreate> FilmCalendarOfFilm(int idFilm);
        void SaveChanges();
    }
    public class FilmCalendarCreateService: IFilmCalendarCreateService
    {
        private IFilmCalendarCreateRepository _filmCalendarCreateRepository;
        private IUnitOfWork _unitOfWork;

        public FilmCalendarCreateService(IFilmCalendarCreateRepository filmCalendarCreateRepository, IUnitOfWork unitOfWork)
        {
            _filmCalendarCreateRepository = filmCalendarCreateRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FilmCalendarCreate> FilmCalendarOfFilm(int idFilm)
        {
            return _filmCalendarCreateRepository.GetAllCalendarCreatesFilm(idFilm);
        }

        public FilmCalendarCreate FindLastCalendarCreateFilm(int idFilm, int idCinema)
        {
            var result = _filmCalendarCreateRepository.FindLastCalendarCreateFilm(idFilm, idCinema);
            return result;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
