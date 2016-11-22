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
    public interface IFilmCategoryService
    {
        // triển khai các phương thức của service
        IEnumerable<FilmCategory> GetAll();

        IEnumerable<FilmCategory> GetFilmCategoryPaging(int page, int pageSize, out int totalRow);

        void SaveChanges();
    }
    public class FilmCategoryService:IFilmCategoryService
    {
        IFilmCategoryRepository _filmCategoryRepository;
        IUnitOfWork _unitOfWork;

        public FilmCategoryService(IFilmCategoryRepository filmCategoryRepository, IUnitOfWork unitOfWork)
        {
            _filmCategoryRepository = filmCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<FilmCategory> GetAll()
        {
            return _filmCategoryRepository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        IEnumerable<FilmCategory> IFilmCategoryService.GetFilmCategoryPaging(int page, int pageSize, out int totalRow)
        {
            var query = _filmCategoryRepository.GetAll();
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
