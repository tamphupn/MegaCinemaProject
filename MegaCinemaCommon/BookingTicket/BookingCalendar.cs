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
        public int TimeBookingSession { get; set; }
        public string TimeBookingDetail { get; set; }
        public int RoomFilmID { get; set; }
    }

    /// <summary>
    /// Defined Booking calendar of Selected day
    /// </summary>
    public class BookingDate
    {
        public DateTime DateBookingStart { get; set; }
        public int DateQuantity { get; set; }
        public List<BookingTime> BookingTicketCalendar { get; set; }
    }

    /// <summary>
    /// Defined Session create Calendar of Admin
    /// </summary>
    public class BookingPlan
    {
        public DateTime DateCreatePlan { get; set; }
        public List<BookingDate> PlanCalendar { get; set; }
    }
}
