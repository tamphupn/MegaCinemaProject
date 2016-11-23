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

    public interface ICinemaService
    {
        //defined method
        IEnumerable<Cinema> GetAll();
        Cinema Add(Cinema cinema);
        void SaveChanges();
    }

    public class CinemaService : ICinemaService
    {
        private ICinemaRepository _cinemaRepository;
        private IUnitOfWork _unitOfWork;

        public CinemaService(ICinemaRepository cinemaRepository, IUnitOfWork unitOfWork)
        {
            _cinemaRepository = cinemaRepository;
            _unitOfWork = unitOfWork;
        }

        public Cinema Add(Cinema cinema)
        {
            return _cinemaRepository.Add(cinema);
        }

        public IEnumerable<Cinema> GetAll()
        {
            return _cinemaRepository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
