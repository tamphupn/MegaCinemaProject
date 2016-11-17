using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;
using MegaCinemaModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaService
{
    public interface IFilmService
    {
        IEnumerable<Film> GetAll();
        Film Add(Film film);
        void SaveChanges();
    }

    public class FilmService : IFilmService
    {
        //implement method
        private IFilmRepository _filmRepository;
        private IUnitOfWork _unitOfWork;

        public FilmService(IFilmRepository filmRepository, IUnitOfWork unitOfWork)
        {
            _filmRepository = filmRepository;
            _unitOfWork = unitOfWork;
        }

        //Method
        public Film Add(Film film)
        {
            return _filmRepository.Add(film);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Film> GetAll()
        {
            return _filmRepository.GetAll();
        }
    }
}
