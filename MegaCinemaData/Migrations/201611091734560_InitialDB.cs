namespace MegaCinemaData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountTypes",
                c => new
                    {
                        TypeID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 100),
                        TypePoint = c.Int(nullable: false),
                        TypeDiscount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.TypeID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerPrefix = c.String(nullable: false, maxLength: 3),
                        CustomerCode = c.String(maxLength: 100),
                        CustomerPoint = c.Int(nullable: false),
                        CustomerAvatar = c.String(maxLength: 100),
                        CustomerIP = c.String(maxLength: 100),
                        CustomerAccountType = c.Int(nullable: false),
                        CustomerStatus = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Statuss", t => t.CustomerStatus)
                .ForeignKey("dbo.AccountTypes", t => t.CustomerAccountType)
                .Index(t => t.CustomerAccountType)
                .Index(t => t.CustomerStatus);
            
            CreateTable(
                "dbo.BookingTickets",
                c => new
                    {
                        BookingTicketID = c.Int(nullable: false, identity: true),
                        BookingTicketPrefix = c.String(nullable: false, maxLength: 3),
                        BookingTicketCode = c.String(),
                        BookingTicketFilmID = c.Int(nullable: false),
                        BookingTicketRoomID = c.Int(nullable: false),
                        BookingTicketTimeDetail = c.String(nullable: false, maxLength: 100),
                        BookingTicketPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        BookingPaymentDate = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        BookingTicketStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.BookingTicketID)
                .ForeignKey("dbo.Films", t => t.BookingTicketFilmID)
                .ForeignKey("dbo.Statuss", t => t.BookingTicketStatusID)
                .ForeignKey("dbo.RoomFilms", t => t.BookingTicketRoomID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .Index(t => t.BookingTicketFilmID)
                .Index(t => t.BookingTicketRoomID)
                .Index(t => t.CustomerID)
                .Index(t => t.BookingTicketStatusID);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        FilmID = c.Int(nullable: false, identity: true),
                        FilmPrefix = c.String(nullable: false, maxLength: 3),
                        FilmCode = c.String(maxLength: 100),
                        FilmName = c.String(nullable: false, maxLength: 100),
                        FilmDuration = c.Int(nullable: false),
                        FilmFirstPremiered = c.DateTime(nullable: false),
                        FilmLanguage = c.String(nullable: false, maxLength: 100),
                        FilmContent = c.String(nullable: false, maxLength: 100),
                        FilmLastPremiered = c.DateTime(nullable: false),
                        FilmPoster = c.String(nullable: false, maxLength: 100),
                        FilmCompanyRelease = c.String(nullable: false, maxLength: 100),
                        FilmTrailer = c.String(maxLength: 100),
                        FilmRatingID = c.Int(nullable: false),
                        FilmStatus = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FilmID)
                .ForeignKey("dbo.FilmRatings", t => t.FilmRatingID)
                .ForeignKey("dbo.Statuss", t => t.FilmStatus)
                .Index(t => t.FilmRatingID)
                .Index(t => t.FilmStatus);
            
            CreateTable(
                "dbo.DetailCategories",
                c => new
                    {
                        FilmID = c.Int(nullable: false),
                        FilmCategoryID = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => new { t.FilmID, t.FilmCategoryID })
                .ForeignKey("dbo.FilmCategories", t => t.FilmCategoryID)
                .ForeignKey("dbo.Films", t => t.FilmID)
                .Index(t => t.FilmID)
                .Index(t => t.FilmCategoryID);
            
            CreateTable(
                "dbo.FilmCategories",
                c => new
                    {
                        FilmCategoryID = c.Int(nullable: false, identity: true),
                        FilmCategoryName = c.String(nullable: false, maxLength: 100),
                        FilmCategoryDescrip = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FilmCategoryID);
            
            CreateTable(
                "dbo.DetailFormats",
                c => new
                    {
                        FilmID = c.Int(nullable: false),
                        FilmFormatID = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => new { t.FilmID, t.FilmFormatID })
                .ForeignKey("dbo.FilmFormats", t => t.FilmFormatID)
                .ForeignKey("dbo.Films", t => t.FilmID)
                .Index(t => t.FilmID)
                .Index(t => t.FilmFormatID);
            
            CreateTable(
                "dbo.FilmFormats",
                c => new
                    {
                        FilmFormatID = c.Int(nullable: false, identity: true),
                        FilmFormatName = c.String(nullable: false, maxLength: 100),
                        FilmFormatDescrip = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FilmFormatID);
            
            CreateTable(
                "dbo.FilmRatings",
                c => new
                    {
                        RatingID = c.Int(nullable: false, identity: true),
                        RatingName = c.String(nullable: false, maxLength: 100),
                        RatingDescription = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.RatingID);
            
            CreateTable(
                "dbo.FilmSessions",
                c => new
                    {
                        FilmSessionID = c.Int(nullable: false, identity: true),
                        FilmID = c.Int(nullable: false),
                        CinemaID = c.Int(nullable: false),
                        FilmCalendar = c.String(nullable: false),
                        DateStartSession = c.DateTime(nullable: false),
                        DateFinishSession = c.DateTime(nullable: false),
                        FilmSessionStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FilmSessionID)
                .ForeignKey("dbo.Cinemas", t => t.CinemaID)
                .ForeignKey("dbo.Statuss", t => t.FilmSessionStatusID)
                .ForeignKey("dbo.Films", t => t.FilmID)
                .Index(t => t.FilmID)
                .Index(t => t.CinemaID)
                .Index(t => t.FilmSessionStatusID);
            
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        CinemaID = c.Int(nullable: false, identity: true),
                        CinemaPrefix = c.String(nullable: false, maxLength: 3),
                        CinemaCode = c.String(maxLength: 100),
                        CinemaFullName = c.String(nullable: false, maxLength: 100),
                        CinemaAddress = c.String(nullable: false, maxLength: 100),
                        CinemaPhone = c.String(nullable: false, maxLength: 15),
                        CinemaEmail = c.String(nullable: false, maxLength: 100),
                        CinemaManagerID = c.Int(nullable: false),
                        CinemaStatus = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.CinemaID)
                .ForeignKey("dbo.Statuss", t => t.CinemaStatus)
                .ForeignKey("dbo.Staffs", t => t.CinemaManagerID)
                .Index(t => t.CinemaManagerID)
                .Index(t => t.CinemaStatus);
            
            CreateTable(
                "dbo.FeatureDetails",
                c => new
                    {
                        FeatureID = c.Int(nullable: false),
                        CinemaID = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => new { t.FeatureID, t.CinemaID })
                .ForeignKey("dbo.CinemaFeatures", t => t.FeatureID)
                .ForeignKey("dbo.Cinemas", t => t.CinemaID)
                .Index(t => t.FeatureID)
                .Index(t => t.CinemaID);
            
            CreateTable(
                "dbo.CinemaFeatures",
                c => new
                    {
                        FeatureID = c.Int(nullable: false, identity: true),
                        FeatureType = c.Boolean(nullable: false),
                        FeatureContent = c.String(nullable: false, maxLength: 100),
                        FeatureDescription = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FeatureID);
            
            CreateTable(
                "dbo.PromotionCines",
                c => new
                    {
                        PromotionID = c.Int(nullable: false),
                        CinemaID = c.Int(nullable: false),
                        Description = c.String(),
                        PromotionCineStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => new { t.PromotionID, t.CinemaID })
                .ForeignKey("dbo.Promotions", t => t.PromotionID)
                .ForeignKey("dbo.Statuss", t => t.PromotionCineStatusID)
                .ForeignKey("dbo.Cinemas", t => t.CinemaID)
                .Index(t => t.PromotionID)
                .Index(t => t.CinemaID)
                .Index(t => t.PromotionCineStatusID);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        PromotionID = c.Int(nullable: false, identity: true),
                        PromotionHeader = c.String(nullable: false, maxLength: 100),
                        PromotionContent = c.String(nullable: false),
                        PromotionPoster = c.String(nullable: false, maxLength: 100),
                        PromotionDateFinish = c.DateTime(nullable: false),
                        PromotionStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.PromotionID)
                .ForeignKey("dbo.Statuss", t => t.PromotionStatusID)
                .Index(t => t.PromotionStatusID);
            
            CreateTable(
                "dbo.Statuss",
                c => new
                    {
                        StatusID = c.String(nullable: false, maxLength: 3),
                        StatusName = c.String(maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.FoodLists",
                c => new
                    {
                        FoodID = c.Int(nullable: false, identity: true),
                        FoodPrefix = c.String(nullable: false, maxLength: 3),
                        FoodCode = c.String(maxLength: 100),
                        FoodName = c.String(nullable: false, maxLength: 100),
                        FoodPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        FoodPoster = c.String(maxLength: 100),
                        FoodStatus = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.FoodID)
                .ForeignKey("dbo.Statuss", t => t.FoodStatus)
                .Index(t => t.FoodStatus);
            
            CreateTable(
                "dbo.TicketCombos",
                c => new
                    {
                        BookingTicketID = c.Int(nullable: false),
                        FoodListID = c.Int(nullable: false),
                        FoodQuantity = c.Int(nullable: false),
                        FoodPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        FoodDiscount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        FoodTotalPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        FoodStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => new { t.BookingTicketID, t.FoodListID })
                .ForeignKey("dbo.FoodLists", t => t.FoodListID)
                .ForeignKey("dbo.Statuss", t => t.FoodStatusID)
                .ForeignKey("dbo.BookingTickets", t => t.BookingTicketID)
                .Index(t => t.BookingTicketID)
                .Index(t => t.FoodListID)
                .Index(t => t.FoodStatusID);
            
            CreateTable(
                "dbo.RoomFilms",
                c => new
                    {
                        RoomID = c.Int(nullable: false, identity: true),
                        RoomPrefix = c.String(nullable: false, maxLength: 3),
                        RoomCode = c.String(maxLength: 100),
                        RoomName = c.String(nullable: false, maxLength: 100),
                        RoomSeatPosition = c.String(nullable: false),
                        RoomCinemaDescription = c.String(),
                        RoomPoster = c.String(maxLength: 100),
                        RoomCinemaID = c.Int(nullable: false),
                        RoomStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.RoomID)
                .ForeignKey("dbo.Statuss", t => t.RoomStatusID)
                .ForeignKey("dbo.Cinemas", t => t.RoomCinemaID)
                .Index(t => t.RoomCinemaID)
                .Index(t => t.RoomStatusID);
            
            CreateTable(
                "dbo.SeatMaintenances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SeatID = c.Int(nullable: false),
                        RoomID = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                        SeatStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SeatLists", t => t.SeatID)
                .ForeignKey("dbo.RoomFilms", t => t.RoomID)
                .ForeignKey("dbo.Statuss", t => t.SeatStatusID)
                .Index(t => t.SeatID)
                .Index(t => t.RoomID)
                .Index(t => t.SeatStatusID);
            
            CreateTable(
                "dbo.SeatLists",
                c => new
                    {
                        SeatID = c.Int(nullable: false, identity: true),
                        SeatTypeID = c.Int(nullable: false),
                        SeatPrefix = c.String(nullable: false, maxLength: 3),
                        SeatCode = c.String(maxLength: 100),
                        SeatName = c.String(nullable: false, maxLength: 100),
                        SeatCoupleTwoID = c.Int(nullable: false),
                        SeatRoomID = c.Int(nullable: false),
                        SeatRow = c.Int(nullable: false),
                        SeatColumn = c.Int(nullable: false),
                        SeatStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.SeatID)
                .ForeignKey("dbo.SeatLists", t => t.SeatCoupleTwoID)
                .ForeignKey("dbo.Statuss", t => t.SeatStatusID)
                .Index(t => t.SeatCoupleTwoID)
                .Index(t => t.SeatStatusID);
            
            CreateTable(
                "dbo.SeatTypes",
                c => new
                    {
                        SeatTypeID = c.Int(nullable: false, identity: true),
                        SeatTypeName = c.String(nullable: false, maxLength: 100),
                        SeatTypeSurcharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SeatTypeStatus = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.SeatTypeID)
                .ForeignKey("dbo.Statuss", t => t.SeatTypeStatus)
                .Index(t => t.SeatTypeStatus);
            
            CreateTable(
                "dbo.TicketDetails",
                c => new
                    {
                        BookingTicketID = c.Int(nullable: false),
                        SeatName = c.String(nullable: false, maxLength: 128),
                        SeatTypeID = c.Int(nullable: false),
                        SeatPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        TicketCategoryID = c.Int(nullable: false),
                        SeatDiscount = c.Decimal(nullable: false, precision: 18, scale: 0),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        TicketStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => new { t.BookingTicketID, t.SeatName })
                .ForeignKey("dbo.TicketCategories", t => t.TicketCategoryID)
                .ForeignKey("dbo.SeatTypes", t => t.SeatTypeID)
                .ForeignKey("dbo.Statuss", t => t.TicketStatusID)
                .ForeignKey("dbo.BookingTickets", t => t.BookingTicketID)
                .Index(t => t.BookingTicketID)
                .Index(t => t.SeatTypeID)
                .Index(t => t.TicketCategoryID)
                .Index(t => t.TicketStatusID);
            
            CreateTable(
                "dbo.TicketCategories",
                c => new
                    {
                        TicketCateID = c.Int(nullable: false, identity: true),
                        TicketCateName = c.String(nullable: false, maxLength: 100),
                        TicketCatePrice = c.Decimal(nullable: false, precision: 18, scale: 0),
                        TicketCateStatusID = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.TicketCateID)
                .ForeignKey("dbo.Statuss", t => t.TicketCateStatusID)
                .Index(t => t.TicketCateStatusID);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffID = c.Int(nullable: false, identity: true),
                        StaffPrefix = c.String(nullable: false, maxLength: 3),
                        StaffCode = c.String(maxLength: 100),
                        StaffRegencyID = c.Int(nullable: false),
                        StaffStatus = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.StaffID)
                .ForeignKey("dbo.Regencies", t => t.StaffRegencyID)
                .ForeignKey("dbo.Statuss", t => t.StaffStatus)
                .Index(t => t.StaffRegencyID)
                .Index(t => t.StaffStatus);
            
            CreateTable(
                "dbo.Regencies",
                c => new
                    {
                        RegencyID = c.Int(nullable: false, identity: true),
                        RegencyName = c.String(nullable: false, maxLength: 100),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.RegencyID);
            
            CreateTable(
                "dbo.TimeSessions",
                c => new
                    {
                        TimeSessionID = c.Int(nullable: false, identity: true),
                        TimeDetail = c.String(nullable: false, maxLength: 100),
                        TimeStatus = c.String(nullable: false, maxLength: 3),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.TimeSessionID)
                .ForeignKey("dbo.Statuss", t => t.TimeStatus)
                .Index(t => t.TimeStatus);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParaName = c.String(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ApplicationUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.ApplicationUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CustomerAccountType", "dbo.AccountTypes");
            DropForeignKey("dbo.BookingTickets", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.TicketDetails", "BookingTicketID", "dbo.BookingTickets");
            DropForeignKey("dbo.TicketCombos", "BookingTicketID", "dbo.BookingTickets");
            DropForeignKey("dbo.FilmSessions", "FilmID", "dbo.Films");
            DropForeignKey("dbo.RoomFilms", "RoomCinemaID", "dbo.Cinemas");
            DropForeignKey("dbo.PromotionCines", "CinemaID", "dbo.Cinemas");
            DropForeignKey("dbo.TimeSessions", "TimeStatus", "dbo.Statuss");
            DropForeignKey("dbo.TicketDetails", "TicketStatusID", "dbo.Statuss");
            DropForeignKey("dbo.TicketCombos", "FoodStatusID", "dbo.Statuss");
            DropForeignKey("dbo.TicketCategories", "TicketCateStatusID", "dbo.Statuss");
            DropForeignKey("dbo.Staffs", "StaffStatus", "dbo.Statuss");
            DropForeignKey("dbo.Staffs", "StaffRegencyID", "dbo.Regencies");
            DropForeignKey("dbo.Cinemas", "CinemaManagerID", "dbo.Staffs");
            DropForeignKey("dbo.SeatTypes", "SeatTypeStatus", "dbo.Statuss");
            DropForeignKey("dbo.TicketDetails", "SeatTypeID", "dbo.SeatTypes");
            DropForeignKey("dbo.TicketDetails", "TicketCategoryID", "dbo.TicketCategories");
            DropForeignKey("dbo.SeatMaintenances", "SeatStatusID", "dbo.Statuss");
            DropForeignKey("dbo.SeatLists", "SeatStatusID", "dbo.Statuss");
            DropForeignKey("dbo.RoomFilms", "RoomStatusID", "dbo.Statuss");
            DropForeignKey("dbo.SeatMaintenances", "RoomID", "dbo.RoomFilms");
            DropForeignKey("dbo.SeatMaintenances", "SeatID", "dbo.SeatLists");
            DropForeignKey("dbo.SeatLists", "SeatCoupleTwoID", "dbo.SeatLists");
            DropForeignKey("dbo.BookingTickets", "BookingTicketRoomID", "dbo.RoomFilms");
            DropForeignKey("dbo.Promotions", "PromotionStatusID", "dbo.Statuss");
            DropForeignKey("dbo.PromotionCines", "PromotionCineStatusID", "dbo.Statuss");
            DropForeignKey("dbo.FoodLists", "FoodStatus", "dbo.Statuss");
            DropForeignKey("dbo.TicketCombos", "FoodListID", "dbo.FoodLists");
            DropForeignKey("dbo.FilmSessions", "FilmSessionStatusID", "dbo.Statuss");
            DropForeignKey("dbo.Films", "FilmStatus", "dbo.Statuss");
            DropForeignKey("dbo.Customers", "CustomerStatus", "dbo.Statuss");
            DropForeignKey("dbo.Cinemas", "CinemaStatus", "dbo.Statuss");
            DropForeignKey("dbo.BookingTickets", "BookingTicketStatusID", "dbo.Statuss");
            DropForeignKey("dbo.PromotionCines", "PromotionID", "dbo.Promotions");
            DropForeignKey("dbo.FilmSessions", "CinemaID", "dbo.Cinemas");
            DropForeignKey("dbo.FeatureDetails", "CinemaID", "dbo.Cinemas");
            DropForeignKey("dbo.FeatureDetails", "FeatureID", "dbo.CinemaFeatures");
            DropForeignKey("dbo.Films", "FilmRatingID", "dbo.FilmRatings");
            DropForeignKey("dbo.DetailFormats", "FilmID", "dbo.Films");
            DropForeignKey("dbo.DetailFormats", "FilmFormatID", "dbo.FilmFormats");
            DropForeignKey("dbo.DetailCategories", "FilmID", "dbo.Films");
            DropForeignKey("dbo.DetailCategories", "FilmCategoryID", "dbo.FilmCategories");
            DropForeignKey("dbo.BookingTickets", "BookingTicketFilmID", "dbo.Films");
            DropIndex("dbo.TimeSessions", new[] { "TimeStatus" });
            DropIndex("dbo.Staffs", new[] { "StaffStatus" });
            DropIndex("dbo.Staffs", new[] { "StaffRegencyID" });
            DropIndex("dbo.TicketCategories", new[] { "TicketCateStatusID" });
            DropIndex("dbo.TicketDetails", new[] { "TicketStatusID" });
            DropIndex("dbo.TicketDetails", new[] { "TicketCategoryID" });
            DropIndex("dbo.TicketDetails", new[] { "SeatTypeID" });
            DropIndex("dbo.TicketDetails", new[] { "BookingTicketID" });
            DropIndex("dbo.SeatTypes", new[] { "SeatTypeStatus" });
            DropIndex("dbo.SeatLists", new[] { "SeatStatusID" });
            DropIndex("dbo.SeatLists", new[] { "SeatCoupleTwoID" });
            DropIndex("dbo.SeatMaintenances", new[] { "SeatStatusID" });
            DropIndex("dbo.SeatMaintenances", new[] { "RoomID" });
            DropIndex("dbo.SeatMaintenances", new[] { "SeatID" });
            DropIndex("dbo.RoomFilms", new[] { "RoomStatusID" });
            DropIndex("dbo.RoomFilms", new[] { "RoomCinemaID" });
            DropIndex("dbo.TicketCombos", new[] { "FoodStatusID" });
            DropIndex("dbo.TicketCombos", new[] { "FoodListID" });
            DropIndex("dbo.TicketCombos", new[] { "BookingTicketID" });
            DropIndex("dbo.FoodLists", new[] { "FoodStatus" });
            DropIndex("dbo.Promotions", new[] { "PromotionStatusID" });
            DropIndex("dbo.PromotionCines", new[] { "PromotionCineStatusID" });
            DropIndex("dbo.PromotionCines", new[] { "CinemaID" });
            DropIndex("dbo.PromotionCines", new[] { "PromotionID" });
            DropIndex("dbo.FeatureDetails", new[] { "CinemaID" });
            DropIndex("dbo.FeatureDetails", new[] { "FeatureID" });
            DropIndex("dbo.Cinemas", new[] { "CinemaStatus" });
            DropIndex("dbo.Cinemas", new[] { "CinemaManagerID" });
            DropIndex("dbo.FilmSessions", new[] { "FilmSessionStatusID" });
            DropIndex("dbo.FilmSessions", new[] { "CinemaID" });
            DropIndex("dbo.FilmSessions", new[] { "FilmID" });
            DropIndex("dbo.DetailFormats", new[] { "FilmFormatID" });
            DropIndex("dbo.DetailFormats", new[] { "FilmID" });
            DropIndex("dbo.DetailCategories", new[] { "FilmCategoryID" });
            DropIndex("dbo.DetailCategories", new[] { "FilmID" });
            DropIndex("dbo.Films", new[] { "FilmStatus" });
            DropIndex("dbo.Films", new[] { "FilmRatingID" });
            DropIndex("dbo.BookingTickets", new[] { "BookingTicketStatusID" });
            DropIndex("dbo.BookingTickets", new[] { "CustomerID" });
            DropIndex("dbo.BookingTickets", new[] { "BookingTicketRoomID" });
            DropIndex("dbo.BookingTickets", new[] { "BookingTicketFilmID" });
            DropIndex("dbo.Customers", new[] { "CustomerStatus" });
            DropIndex("dbo.Customers", new[] { "CustomerAccountType" });
            DropTable("dbo.ApplicationUserLogins");
            DropTable("dbo.ApplicationUserRoles");
            DropTable("dbo.Parameters");
            DropTable("dbo.TimeSessions");
            DropTable("dbo.Regencies");
            DropTable("dbo.Staffs");
            DropTable("dbo.TicketCategories");
            DropTable("dbo.TicketDetails");
            DropTable("dbo.SeatTypes");
            DropTable("dbo.SeatLists");
            DropTable("dbo.SeatMaintenances");
            DropTable("dbo.RoomFilms");
            DropTable("dbo.TicketCombos");
            DropTable("dbo.FoodLists");
            DropTable("dbo.Statuss");
            DropTable("dbo.Promotions");
            DropTable("dbo.PromotionCines");
            DropTable("dbo.CinemaFeatures");
            DropTable("dbo.FeatureDetails");
            DropTable("dbo.Cinemas");
            DropTable("dbo.FilmSessions");
            DropTable("dbo.FilmRatings");
            DropTable("dbo.FilmFormats");
            DropTable("dbo.DetailFormats");
            DropTable("dbo.FilmCategories");
            DropTable("dbo.DetailCategories");
            DropTable("dbo.Films");
            DropTable("dbo.BookingTickets");
            DropTable("dbo.Customers");
            DropTable("dbo.AccountTypes");
        }
    }
}
