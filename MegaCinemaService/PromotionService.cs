using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;

namespace MegaCinemaService
{
    public interface IPromotionService
    {
        // triển khai các phương thức của service
        void SaveChanges();
    }
    public class PromotionService : IPromotionService
    {
        IPromotionRepository _promotionRepository;
        IUnitOfWork _unitOfWork;

        public PromotionService(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
        {
            _promotionRepository = promotionRepository;
            _unitOfWork = unitOfWork;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
