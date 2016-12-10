using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;

namespace MegaCinemaService
{
    public interface IDetailFormatService
    {
        // triển khai các phương thức của service
        void SaveChanges();
    }
    public class DetailFormatService:IDetailFormatService
    {
        IDetailFormatRepository _detailFormatRepository;
        IUnitOfWork _unitOfWork;

        public DetailFormatService(IDetailFormatRepository detailFormatRepository, IUnitOfWork unitOfWork)
        {
            _detailFormatRepository = detailFormatRepository;
            _unitOfWork = unitOfWork;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
