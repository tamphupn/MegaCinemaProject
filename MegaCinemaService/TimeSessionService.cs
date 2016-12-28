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
    public interface ITimeSessionService
    {
        TimeSession Find(int id);
        Film GetFilmDetail(int id);
        void SaveChanges();
    }
    public class TimeSessionService:ITimeSessionService
    {
        private ITimeSessionRepository _timeSessionRepository;
        private IUnitOfWork _unitOfWork;

        public TimeSessionService(ITimeSessionRepository timeSessionRepository, IUnitOfWork unitOfWork)
        {
            _timeSessionRepository = timeSessionRepository;
            _unitOfWork = unitOfWork;
        }

        public TimeSession Find(int id)
        {
            return _timeSessionRepository.GetSingleById(id);
        }

        public Film GetFilmDetail(int id)
        {
            return _timeSessionRepository.GetFilmDetail(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
