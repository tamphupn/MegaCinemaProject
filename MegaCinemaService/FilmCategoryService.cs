using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;

namespace MegaCinemaService
{
    public interface IFilmCategoryService
    {
        // triển khai các phương thức của service
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

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
