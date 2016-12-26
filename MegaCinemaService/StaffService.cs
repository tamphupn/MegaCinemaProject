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
    public interface IStaffService
    {
        IEnumerable<Staff> GetAll();
    }

    public class StaffService : IStaffService
    {
        private IStaffRepository _staffRepository;
        private IUnitOfWork _unitOfWork;

        public StaffService(IStaffRepository staffRepository, IUnitOfWork unitOfWork)
        {
            _staffRepository = staffRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Staff> GetAll()
        {
            return _staffRepository.GetAll();
        }
    }
}
