using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IEventTopicRepository : IRepository<EventTopic>
    {

    }

    public class EventTopicRepository : RepositoryBase<EventTopic>, IEventTopicRepository
    {
        public EventTopicRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
}
