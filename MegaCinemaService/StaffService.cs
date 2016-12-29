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
        ApplicationUser Find(int i);
        ApplicationUser FindApplicationUser(string id);
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

        public ApplicationUser Find(int i)
        {
            return _staffRepository.FindStaff(i);
        }

        public ApplicationUser FindApplicationUser(string id)
        {
            return _staffRepository.FindApplicationUser(id);
        }

        public IEnumerable<Staff> GetAll()
        {
            return _staffRepository.GetAll();
        }
    }
}
