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
        IEnumerable<FilmFormat> GetAll();
        IEnumerable<FilmFormat> GetFilmFormatPaging(int page, int pageSize, out int totalRow);
        void SaveChanges();
    }
    public class FilmFormatService:IFilmFormatService
    {
        IFilmFormatRepository _filmFormatRepository;
        IUnitOfWork _unitOfWork;
        public FilmFormatService(IFilmFormatRepository filmFormatRepository, IUnitOfWork unitOfWork)
        {
            _filmFormatRepository = filmFormatRepository;
            _unitOfWork = unitOfWork;
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
    }
}
