using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaData.Repositories
{
    public interface IBookingTicketRepository : IRepository<BookingTicket>
    {
        
    }
    public class BookingTicketRepository: RepositoryBase<BookingTicket>, IBookingTicketRepository
    {
        public BookingTicketRepository(IDbFactory dbfactory):base(dbfactory)
        {

        }
    }
}
