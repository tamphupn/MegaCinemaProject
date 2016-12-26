using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaData.Infrastructures;
using MegaCinemaData.Repositories;
using MegaCinemaModel.Models;

namespace MegaCinemaService
{
    public interface IEventTopicService
    {
        IEnumerable<EventTopic> GetAll();
    }
    public class EventTopicService : IEventTopicService
    {
        private IEventTopicRepository _eventTopicRepository;
        private IUnitOfWork _unitOfWork;

        public EventTopicService(IEventTopicRepository eventTopicRepository, IUnitOfWork unitOfWork)
        {
            _eventTopicRepository = eventTopicRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EventTopic> GetAll()
        {
            var result = _eventTopicRepository.GetAll();
            if (result.Count() > ParametersContrants.CONTENT_GET)
                return result.Reverse().Take(ParametersContrants.CONTENT_GET).Reverse();
            return result;
        }
    }
}
