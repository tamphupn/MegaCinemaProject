using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;

namespace MegaCinemaService
{
    public interface IDetailCategoryService
    {
        // triển khai các phương thức của service
        void SaveChanges();
    }
    class DetailCategoryService:IDetailCategoryService
    {
        IDetailCategoryRepository _detailCategoryRepository;
        IUnitOfWork _unitOfWork;

        public DetailCategoryService(IDetailCategoryRepository detailCategoryRepository, IUnitOfWork unitOfWork)
        {
            _detailCategoryRepository = _detailCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
