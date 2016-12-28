using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MegaCinemaCommon.BookingTicket
{

    public static class BookingTimeHelpers
    {
        public static string ConvertBookingPlanToJson(BookingPlan bookingPLan)
        {
            var temp = new JavaScriptSerializer().Serialize(bookingPLan);
            return temp;
        }

        public static BookingPlan ConvertJsonToBookingPlan(string jBookingPlan)
        {
            var temp = new JavaScriptSerializer().Deserialize<BookingPlan>(jBookingPlan);
            return temp;
        }


        public static string ConvertBookingTimeToJson(BookingPlan bookingPLan)
        {
            var temp = new JavaScriptSerializer().Serialize(bookingPLan);
            return temp;
        }

        public static BookingPlan ConvertJsonToBookingTime(string jBookingPlan)
        {
            var temp = new JavaScriptSerializer().Deserialize<BookingPlan>(jBookingPlan);
            return temp;
        }

        public static string ConvertCalendarToJson(BookingPlanTime time)
        {
            var temp = new JavaScriptSerializer().Serialize(time);
            return temp;
        }

        public static BookingPlanTime ConvertJsonToCalendar(string json)
        {
            var temp = new JavaScriptSerializer().Deserialize<BookingPlanTime>(json);
            return temp;
        }


        public static string ConvertBookingSessionToJson(SeatState seatState)
        {
            var temp = new JavaScriptSerializer().Serialize(seatState);
            return temp;
        }

        public static SeatState ConvertJsontoBookingTime(string jBookingPlan)
        {
            var temp = new JavaScriptSerializer().Deserialize<SeatState>(jBookingPlan);
            return temp;
        }

        public static SeatState ChangeStateSeat(int? xLocation, int? yLocation, int? type, SeatState seatState)
        {
            int? indexSelect = seatState.Width * yLocation + xLocation;
            seatState.LstSeatStates[(int)indexSelect].StateSeat = (int)type;
            return seatState;
        }

        public static int[][] GenerateSeatState(SeatState seatState)
        {
            int[][] result = new int[seatState.Height][];
            for (int i = 0; i < seatState.Height; i++)
            {
                result[i] = new int[seatState.Width];
            }

            int count = 0;
            for (int i = 0; i < seatState.Height; i++)
                for (int j = 0; j < seatState.Width; j++)
                {
                    result[i][j] = seatState.LstSeatStates[count].StateSeat;
                    count++;
                }

            return result;
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
