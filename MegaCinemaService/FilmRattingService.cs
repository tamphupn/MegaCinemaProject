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
    public interface IFilmRattingService
    {
        IEnumerable<FilmRating> GetAll();
        void SaveChanges();
    }
    public class FilmRattingService : IFilmRattingService
    {
        //implement method
        private IFilmRattingRepository _filmRattingRepository;
        private IUnitOfWork _unitOfWork;

        public FilmRattingService(IFilmRattingRepository filmRattingRepository, IUnitOfWork unitOfWork)
        {
            _filmRattingRepository = filmRattingRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FilmRating> GetAll()
        {
            return _filmRattingRepository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
