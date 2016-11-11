using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Validation;
using MegaCinemaModel.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MegaCinemaData
{
    public class MegaCinemaDBContext : IdentityDbContext<ApplicationUser>
    {
        public MegaCinemaDBContext():base("MegaCinemaDB")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage));
                throw new DbEntityValidationException(errorMessages);
            }
        }
        
        //Entity list
        public DbSet<Status> Statuss { get; set; }
        public DbSet<FilmCategory> FilmCategories { get; set; }
        public DbSet<FilmFormat> FilmFormats { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmRating> FilmRatings { get; set; }
        public DbSet<DetailFormat> DetailFormats { get; set; }
        public DbSet<DetailCategory> DetailCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Regency> Regencies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<CinemaFeature> CinemaFeatures { get; set; }
        public DbSet<FeatureDetail> FeatureDetails { get; set; }
        public DbSet<FoodList> FoodLists { get; set; }
        public DbSet<SeatType> SeatTypes { get; set; }
        public DbSet<RoomFilm> RoomFilms { get; set; }
        public DbSet<SeatList> SeatLists { get; set; }
        public DbSet<TimeSession> TimeSessions { get; set; }
        public DbSet<SeatMaintenance> SeatMaintenances { get; set; }
        public DbSet<FilmSession> FilmSessions { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<BookingTicket> BookingTickets { get; set; }
        public DbSet<TicketDetail> TicketDetails { get; set; }
        public DbSet<TicketCombo> TicketCombos { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionCine> PromotionCines { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        public static MegaCinemaDBContext Create()
        {
            return new MegaCinemaDBContext();
        }
        //Assign attribute
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Asp.net identity
            modelBuilder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");

            //status
            modelBuilder.Entity<Status>()
                .HasMany(e => e.Films)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Staffs)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Cinemas)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.FoodLists)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.SeatTypes)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.RoomFilms)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.SeatLists)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.TimeSessions)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.SeatMaintenances)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.FilmSessions)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.TicketCategories)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.BookingTickets)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.TicketDetails)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.TicketCombos)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Promotions)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.PromotionCines)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            //Film categories
            modelBuilder.Entity<FilmCategory>()
               .HasMany(e => e.DetailCategories)
               .WithRequired(e => e.FilmCategory)
               .WillCascadeOnDelete(false);

            //Film Format
            modelBuilder.Entity<FilmFormat>()
                .HasMany(e => e.DetailFormats)
                .WithRequired(e => e.FilmFormat)
                .WillCascadeOnDelete(false);

            //Film
            modelBuilder.Entity<Film>()
                .HasMany(e => e.DetailCategories)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.DetailFormats)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.FilmSessions)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.BookingTickets)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            //Film rating
            modelBuilder.Entity<FilmRating>()
                .HasMany(e => e.Films)
                .WithRequired(e => e.FilmRating)
                .WillCascadeOnDelete(false);

            //Detail format

            //Detail Category

            //Customer 
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.BookingTickets)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            //Account type
            modelBuilder.Entity<AccountType>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.AccountType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AccountType>()
                .Property(e => e.TypeDiscount)
                .HasPrecision(18, 0);

            //Staff
            modelBuilder.Entity<Staff>()
                .HasMany(e => e.Cinemas)
                .WithRequired(e => e.Staff)
                .WillCascadeOnDelete(false);

            //Regency
            modelBuilder.Entity<Regency>()
                .HasMany(e => e.Staffs)
                .WithRequired(e => e.Regency)
                .WillCascadeOnDelete(false);

            //Cinema
            modelBuilder.Entity<Cinema>()
               .HasMany(e => e.FeatureDetails)
               .WithRequired(e => e.Cinema)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cinema>()
               .HasMany(e => e.RoomFilms)
               .WithRequired(e => e.Cinema)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cinema>()
               .HasMany(e => e.FilmSessions)
               .WithRequired(e => e.Cinema)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cinema>()
               .HasMany(e => e.PromotionCines)
               .WithRequired(e => e.Cinema)
               .WillCascadeOnDelete(false);

            //Cinema Feature
            modelBuilder.Entity<CinemaFeature>()
               .HasMany(e => e.FeatureDetails)
               .WithRequired(e => e.CinemaFeature)
               .WillCascadeOnDelete(false);

            //Feature detail

            //Food Lists
            modelBuilder.Entity<FoodList>()
               .HasMany(e => e.TicketCombos)
               .WithRequired(e => e.FoodList)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<FoodList>()
                .Property(e => e.FoodPrice)
                .HasPrecision(18, 0);

            //Seat Type
            modelBuilder.Entity<SeatType>()
               .HasMany(e => e.TicketDetails)
               .WithRequired(e => e.SeatType)
               .WillCascadeOnDelete(false);

            //Room Film
            modelBuilder.Entity<RoomFilm>()
               .HasMany(e => e.SeatMaintenances)
               .WithRequired(e => e.RoomFilm)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomFilm>()
               .HasMany(e => e.BookingTickets)
               .WithRequired(e => e.RoomFilm)
               .WillCascadeOnDelete(false);

            //Seat List
            modelBuilder.Entity<SeatList>()
               .HasMany(e => e.SeatMaintenances)
               .WithRequired(e => e.SeatList)
               .WillCascadeOnDelete(false);

            //Time Session

            //Seat Maintenance

            //Film session

            //Ticket Categories
            modelBuilder.Entity<TicketCategory>()
               .HasMany(e => e.TicketDetails)
               .WithRequired(e => e.TicketCategory)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<TicketCategory>()
                .Property(e => e.TicketCatePrice)
                .HasPrecision(18, 0);

            //Booking ticket
            modelBuilder.Entity<BookingTicket>()
               .HasMany(e => e.TicketDetails)
               .WithRequired(e => e.BookingTicket)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<BookingTicket>()
               .HasMany(e => e.TicketCombos)
               .WithRequired(e => e.BookingTicket)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<BookingTicket>()
                .Property(e => e.BookingTicketPrice)
                .HasPrecision(18, 0);

            //Ticket Detail
            modelBuilder.Entity<TicketDetail>()
                .Property(e => e.SeatPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TicketDetail>()
                .Property(e => e.SeatDiscount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TicketDetail>()
                .Property(e => e.TotalPrice)
                .HasPrecision(18, 0);

            //Ticket Combo
            modelBuilder.Entity<TicketCombo>()
                .Property(e => e.FoodPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TicketCombo>()
                .Property(e => e.FoodDiscount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TicketCombo>()
                .Property(e => e.FoodTotalPrice)
                .HasPrecision(18, 0);

            //Promotion 
            modelBuilder.Entity<Promotion>()
               .HasMany(e => e.PromotionCines)
               .WithRequired(e => e.Promotion)
               .WillCascadeOnDelete(false);

            //Promotion Cinema

            //Parameters
        }
    }
}
