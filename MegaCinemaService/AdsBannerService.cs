using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;
using MegaCinemaModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaCommon.StatusCommon;

namespace MegaCinemaService
{
    public interface IAdsBannerService
    {
        IEnumerable<AdsBanner> GetAll();
    }
    public class AdsBannerService : IAdsBannerService
    {
        private IAdsBannerRepository _adsBannerRepository;
        private IUnitOfWork _unitOfWork;

        public AdsBannerService(IAdsBannerRepository adsBannerRepository, IUnitOfWork unitOfWork)
        {
            _adsBannerRepository = adsBannerRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AdsBanner> GetAll()
        {
            var result = _adsBannerRepository.GetAll();
            if (result.Count() > ParametersContrants.CONTENT_GET)
                return result.Reverse().Take(ParametersContrants.CONTENT_GET).Reverse();
            return result;
        }
    }
}
