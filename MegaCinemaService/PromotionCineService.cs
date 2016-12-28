using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Repositories;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaService
{
    public interface IPromotionCineService
    {
        PromotionCine Add(PromotionCine promotionCine);
        IEnumerable<Cinema> GetAllCinemaFullName();
        IEnumerable<Promotion> GetAllPromotionHeader();
        //triển khai các phương thức của service
        void SaveChanges();
        IEnumerable<PromotionCine> GetAll();
    }
    public class PromotionCineService : IPromotionCineService
    {
        private IPromotionCineRepository _promotionCineRepository;
        private IUnitOfWork _unitOfWork;
        private ICinemaRepository _cinemaRepository;
        private IPromotionRepository _promotionRepository;

        public PromotionCineService(IPromotionCineRepository promotionCineRepository, IUnitOfWork unitOfWork, ICinemaRepository cinemaRepository, IPromotionRepository promotionRepository)
        {
            _promotionCineRepository = promotionCineRepository;
            _unitOfWork = unitOfWork;
            _cinemaRepository = cinemaRepository;
            _promotionRepository = promotionRepository;
        }

        public PromotionCine Add(PromotionCine promotionCine)
        {
            return _promotionCineRepository.Add(promotionCine);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Cinema> GetAllCinemaFullName()
        {
            return _cinemaRepository.GetAll();
        }

        public IEnumerable<Promotion> GetAllPromotionHeader()
        {
            return _promotionRepository.GetAll();
        }

        public IEnumerable<PromotionCine> GetAll()
        {
            var result = _promotionCineRepository.GetAll();
            return result;
        }
    }
}
