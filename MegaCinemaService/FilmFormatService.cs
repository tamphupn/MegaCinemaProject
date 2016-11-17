using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;

namespace MegaCinemaService
{
    public interface IFilmFormatService
    {
        // triển khai các phương thức của service
        void SaveChanges();
    }
    public class FilmFormatService:IFilmFormatService
    {
        IFilmFormatService _filmFormatRepository;
        IUnitOfWork _unitOfWork;
        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
