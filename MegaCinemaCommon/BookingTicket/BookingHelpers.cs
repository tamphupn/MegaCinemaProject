using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MegaCinemaCommon.BookingTicket
{
    interface IBookingTimeHelpers
    {
        //BookingPlan GenerateSampleData();
        string ConvertBookingTimeToJson(BookingPlan bookingPLan);
        BookingPlan ConvertJsonToBookingTime(string jBookingPlan);
    }
    public class BookingTimeHelpers : IBookingTimeHelpers
    {
        public string ConvertBookingTimeToJson(BookingPlan bookingPLan)
        {
            var temp = new JavaScriptSerializer().Serialize(bookingPLan);
            return temp;
        }

        public BookingPlan ConvertJsonToBookingTime(string jBookingPlan)
        {
            var temp = new JavaScriptSerializer().Deserialize<BookingPlan>(jBookingPlan);
            return temp;
        }
        //public BookingPlan GenerateSampleDataVersonSecond()
        //{
        //    BookingPlan value = new BookingPlan();
        //    value.DateCreatePlan = DateTime.ParseExact("02/11/2016", "dd/mm/yyyy", null);

        //    //list booking time
        //    List<BookingTime> lstBookingTime = new List<BookingTime>();
        //    lstBookingTime.Add(new BookingTime() { TimeBookingID = 6, TimeBookingDetail = "9:30:00", RoomFilmID = 2, TimeBookingSession = 6 });
        //    lstBookingTime.Add(new BookingTime() { TimeBookingID = 7, TimeBookingDetail = "11:30:00", RoomFilmID = 2, TimeBookingSession = 7 });
        //    lstBookingTime.Add(new BookingTime() { TimeBookingID = 8, TimeBookingDetail = "2:30:00", RoomFilmID = 2, TimeBookingSession = 8 });
        //    lstBookingTime.Add(new BookingTime() { TimeBookingID = 9, TimeBookingDetail = "17:30:00", RoomFilmID = 2, TimeBookingSession = 9 });

        //    //list booking date
        //    List<BookingDate> lstBookingDate = new List<BookingDate>();
        //    lstBookingDate.Add(new BookingDate() { DateBooking = DateTime.ParseExact("03/11/2016", "dd/mm/yyyy", null), BookingTicketCalendar = lstBookingTime });
        //    lstBookingDate.Add(new BookingDate() { DateBooking = DateTime.ParseExact("04/11/2016", "dd/mm/yyyy", null), BookingTicketCalendar = lstBookingTime });
        //    lstBookingDate.Add(new BookingDate() { DateBooking = DateTime.ParseExact("05/11/2016", "dd/mm/yyyy", null), BookingTicketCalendar = lstBookingTime });
        //    lstBookingDate.Add(new BookingDate() { DateBooking = DateTime.ParseExact("06/11/2016", "dd/mm/yyyy", null), BookingTicketCalendar = lstBookingTime });

        //    //add booking plan
        //    value.PlanCalendar = lstBookingDate;
        //    return value;
        //}
        //public BookingPlan GenerateSampleData()
        //{
        //    BookingPlan value = new BookingPlan();
        //    value.DateCreatePlan = DateTime.ParseExact("29/10/2016", "dd/mm/yyyy", null);

        //    //list booking time
        //    List<BookingTime> lstBookingTime = new List<BookingTime>();
        //    lstBookingTime.Add(new BookingTime() { TimeBookingID = 1, TimeBookingDetail = "9:30:00", RoomFilmID = 1, TimeBookingSession = 1 });
        //    lstBookingTime.Add(new BookingTime() { TimeBookingID = 2, TimeBookingDetail = "11:30:00", RoomFilmID = 1, TimeBookingSession = 2 });
        //    lstBookingTime.Add(new BookingTime() { TimeBookingID = 3, TimeBookingDetail = "2:30:00", RoomFilmID = 2, TimeBookingSession = 1 });
        //    lstBookingTime.Add(new BookingTime() { TimeBookingID = 4, TimeBookingDetail = "17:30:00", RoomFilmID = 1, TimeBookingSession = 4 });

        //    //list booking date
        //    List<BookingDate> lstBookingDate = new List<BookingDate>();
        //    lstBookingDate.Add(new BookingDate() { DateBooking = DateTime.ParseExact("30/10/2016", "dd/mm/yyyy", null), BookingTicketCalendar = lstBookingTime });
        //    lstBookingDate.Add(new BookingDate() { DateBooking = DateTime.ParseExact("31/10/2016", "dd/mm/yyyy", null), BookingTicketCalendar = lstBookingTime });
        //    lstBookingDate.Add(new BookingDate() { DateBooking = DateTime.ParseExact("01/11/2016", "dd/mm/yyyy", null), BookingTicketCalendar = lstBookingTime });
        //    lstBookingDate.Add(new BookingDate() { DateBooking = DateTime.ParseExact("02/11/2016", "dd/mm/yyyy", null), BookingTicketCalendar = lstBookingTime });

        //    //add booking plan
        //    value.PlanCalendar = lstBookingDate;
        //    return value;
        //}
    }
}
