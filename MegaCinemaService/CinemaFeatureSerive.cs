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
    public interface ICinemaFeatureService
    {
        //defined method
        IEnumerable<CinemaFeature> GetAll();
        CinemaFeature Add(CinemaFeature cinemaFeature);
        void SaveChanges();
    }

    public class CinemaFeatureService : ICinemaFeatureService
    {
        private ICinemaFeatureRepository _cinemaFeatureRepository;
        private IUnitOfWork _unitOfWork;

        public CinemaFeatureService(ICinemaFeatureRepository cinemaFeatureRepository, IUnitOfWork unitOfWork)
        {
            _cinemaFeatureRepository = cinemaFeatureRepository;
            _unitOfWork = unitOfWork;
        }
        public CinemaFeature Add(CinemaFeature cinemaFeature)
        {
            return _cinemaFeatureRepository.Add(cinemaFeature);
        }

        public IEnumerable<CinemaFeature> GetAll()
        {
            return _cinemaFeatureRepository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
