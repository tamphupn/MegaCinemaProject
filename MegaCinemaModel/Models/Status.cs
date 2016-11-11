using MegaCinemaModel.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaCinemaModel.Models
{
    [Table("Statuss")]
    public class Status : Auditable
    {
        [Key]
        [DataType("nvarchar")]
        [MaxLength(3)]
        public string StatusID { get; set; }

        [DataType("nvarchar")]
        [MaxLength(100)]
        public string StatusName { get; set; }

        [DataType("nvarchar"),MaxLength(100)]
        public string StatusDescription { get; set; }

        public virtual ICollection<Film> Films { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
        public virtual ICollection<Cinema> Cinemas { get; set; }
        public virtual ICollection<FoodList> FoodLists { get; set; }
        public virtual ICollection<SeatType> SeatTypes { get; set; }
        public virtual ICollection<RoomFilm> RoomFilms { get; set; }
        public virtual ICollection<SeatList> SeatLists { get; set; }
        public virtual ICollection<TimeSession> TimeSessions { get; set; }
        public virtual ICollection<SeatMaintenance> SeatMaintenances { get; set; }
        public virtual ICollection<FilmSession> FilmSessions { get; set; }
        public virtual ICollection<TicketCategory> TicketCategories { get; set; }
        public virtual ICollection<BookingTicket> BookingTickets { get; set; }
        public virtual ICollection<TicketDetail> TicketDetails { get; set; }
        public virtual ICollection<TicketCombo> TicketCombos { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
        public virtual ICollection<PromotionCine> PromotionCines { get; set; }
    }
}