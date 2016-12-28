using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaCommon.BookingTicket
{
    /// <summary>
    /// Defined selected time booking in day
    /// </summary>
    public class BookingTime
    {
        public int TimeBookingID { get; set; }
        public string TimeBookingDetail { get; set; }
    }

    /// <summary>
    /// Defined Session create Calendar of Admin
    /// </summary>
    public class BookingPlan
    {
        public int DateQuantity { get; set; }
        public List<BookingTime> BookingTicketCalendar { get; set; }

        public BookingPlan()
        {
            BookingTicketCalendar = new List<BookingTime>();
        }
    }

    public class BookingPlanTime
    {
        public List<BookingPlan> BookingTicketTime { get; set; }

        public BookingPlanTime()
        {
            BookingTicketTime = new List<BookingPlan>();
        }
    }
    /// <summary>
    /// Defined state of seat in room film
    /// 0: Seat booked
    /// 1: Normal Seat
    /// 2: Hot Seat 
    /// 3: Couple Seat
    /// 4: None
    /// </summary>
    public class SeatInstance
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int StateSeat { get; set; }
    }

    public class SeatState
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<SeatInstance> LstSeatStates { get; set; }
    }
}
