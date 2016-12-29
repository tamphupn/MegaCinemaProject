using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaCommon.BookingTicket;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaData.Repositories;
using MegaCinemaData.Infrastructures;
using MegaCinemaModel.Models;

namespace MegaCinemaService
{
    public interface IBookingTicketService
    {
        bool AddTicketToDB(Film film, int customerID, int timeSessionFilm, SeatState seatState);
        IEnumerable<BookingTicket> GetAll();
        void SaveChanges();
        IEnumerable<BookingTicket> GetTicketListPaging(int page, int pageSize, out int totalRow);

    }
    public class BookingTicketService: IBookingTicketService
    {
        private IBookingTicketRepository _bookingTicketRepository;
        private ITimeSessionRepository _timeSessionRepository;
        private IUnitOfWork _unitOfWork;

        public BookingTicketService(IBookingTicketRepository bookingTicketRepository, IUnitOfWork unitOfWork, ITimeSessionRepository timeSessionRepository)
        {
            _bookingTicketRepository = bookingTicketRepository;
            _unitOfWork = unitOfWork;
            _timeSessionRepository = timeSessionRepository;
        }

        public bool AddTicketToDB(Film film, int customerID, int timeSessionFilm, SeatState seatState)
        {
            //get time session            
            BookingTicket result = new BookingTicket();

            try
            {
                var resultTimeSession = _timeSessionRepository.GetSingleById(timeSessionFilm);

                //ticket detail
                result.BookingTicketPrefix = "TIC";
                result.BookingTicketFilmID = film.FilmID;
                result.BookingTicketPrice = 0;
                result.BookingTicketRoomID = 1;
                result.BookingPaymentDate = DateTime.Now;
                result.BookingTicketStatusID = StatusCommonConstrants.ACTIVE;
                result.BookingTicketTimeDetail = resultTimeSession.TimeDetail;
                result.CustomerID = customerID;

                //state seat detail
                resultTimeSession.SeatTableState = BookingTimeHelpers.ConvertBookingSessionToJson(seatState);

                //add
                _bookingTicketRepository.Add(result);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTran();
                return false;
            }
            return false;
        }

        public IEnumerable<BookingTicket> GetAll()
        {
            return _bookingTicketRepository.GetAll();
        }

        public IEnumerable<BookingTicket> GetTicketListPaging(int page, int pageSize, out int totalRow)
        {
            var query = _bookingTicketRepository.GetMulti(x => x.BookingTicketStatusID == "AC");
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
