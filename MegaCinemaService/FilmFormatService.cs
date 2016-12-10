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
    public interface IFilmFormatService
    {
        // triển khai các phương thức của service
        FilmFormat Add(FilmFormat filmFormat);
        IEnumerable<FilmFormat> GetAll();
        IEnumerable<FilmFormat> GetFilmFormatPaging(int page, int pageSize, out int totalRow);
        void SaveChanges();
        FilmFormat Find(int id);
        void Update(FilmFormat filmFormat);
        FilmFormat Delete(FilmFormat filmFormat);

    }
    public class FilmFormatService : IFilmFormatService
    {
        IFilmFormatRepository _filmFormatRepository;
        IUnitOfWork _unitOfWork;
        public FilmFormatService(IFilmFormatRepository filmFormatRepository, IUnitOfWork unitOfWork)
        {
            _filmFormatRepository = filmFormatRepository;
            _unitOfWork = unitOfWork;
        }

        public FilmFormat Add(FilmFormat filmFormat)
        {
            return _filmFormatRepository.Add(filmFormat);
        }

        public FilmFormat Delete(FilmFormat filmFormat)
        {
            return _filmFormatRepository.Delete(filmFormat);
        }

        public FilmFormat Find(int id)
        {
            return _filmFormatRepository.GetSingleById(id);
        }

        public IEnumerable<FilmFormat> GetAll()
        {
            return _filmFormatRepository.GetAll();
        }

        public IEnumerable<FilmFormat> GetFilmFormatPaging(int page, int pageSize, out int totalRow)
        {
            var query = _filmFormatRepository.GetAll();
                totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(FilmFormat filmFormat)
        {
            _filmFormatRepository.Update(filmFormat);
        }
    }
}