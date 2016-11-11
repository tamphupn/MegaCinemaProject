using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;
using MegaCinemaModel.Models;

namespace MegaCinemaService
{
    public interface IStatusService
    {
        //defined method 
        IEnumerable<Status> GetAll();
    }
    public class StatusService : IStatusService
    {
        //implement method
        private IStatusRepository _statusRepository;
        private IUnitOfWork _unitOfWork;

        public StatusService(IStatusRepository statusRepository, IUnitOfWork unitOfWork)
        {
            _statusRepository = statusRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Status> GetAll()
        {
            return _statusRepository.GetAll();
        }
    }
}
