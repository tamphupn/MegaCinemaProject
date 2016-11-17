using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Repositories;
using MegaCinemaData.Infrastructures;

namespace MegaCinemaService
{
    public interface IPromotionCineService
    {
        //triển khai các phương thức của service
        void SaveChanges();
    }
    public class PromotionCineService : IPromotionService
    {
        private IPromotionCineRepository _promotionCineRepository;
        private IUnitOfWork _unitOfWork;

        public PromotionCineService(IPromotionCineRepository promotionCineRepository, IUnitOfWork unitOfWork)
        {
            _promotionCineRepository = promotionCineRepository;
            _unitOfWork = unitOfWork;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
