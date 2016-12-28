using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaCommon.BookingTicket;
using MegaCinemaModel.Models;
using MegaCinemaCommon.StatusCommon;
using MegaCinemaCommon.BookingTicket;

namespace MegaCinemaData.SampleData
{
    public static class SampleData
    {
        public static void GenerateRegency(MegaCinemaDBContext context)
        {
            context.Regencies.Add(new Regency { RegencyName = "Quản trị hệ thống", CreatedDate = DateTime.Now });
            context.Regencies.Add(new Regency { RegencyName = "Quản lý rạp phim", CreatedDate = DateTime.Now });
        }
        public static void GenerateStaff(MegaCinemaDBContext context)
        {
            context.Users.Add(new ApplicationUser
            {
                FirstName = "Nghĩa",
                LastName = "Nguyễn Văn",
                Birthday = DateTime.Parse("18/10/1995"),
                Sex = true,
                SSN = "22150678",
                Address = "KTX khu B, DHQG TPHCM",
                District = "Thủ đức",
                City = "Hồ chí minh",
                Email = "nghiauit@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0123456789",
                PasswordHash = "1234567a",
                UserName = "nguyennghia1",
                Staff = new Staff
                {
                    StaffPrefix = CommonConstrants.STAFF_PREFIX,
                    StaffRegencyID = 1,
                    StaffStatus = "AC",
                },
            });
        }
        public static void GenerateStatus(MegaCinemaDBContext context)
        {
            context.Statuss.Add(new Status { StatusID = "NOT", StatusName = "Không hoạt động", CreatedDate = DateTime.Now });
            context.Statuss.Add(new Status { StatusID = "ACT", StatusName = "Đã kích hoạt", CreatedDate = DateTime.Now });
            context.Statuss.Add(new Status { StatusID = "NAT", StatusName = "Chưa kích hoạt", CreatedDate = DateTime.Now });
            context.Statuss.Add(new Status { StatusID = "AC", StatusName = "Đang hoạt động", CreatedDate = DateTime.Now });

            context.Statuss.Add(new Status { StatusID = "PEN", StatusName = "Đang công chiếu", CreatedDate = DateTime.Now });
            context.Statuss.Add(new Status { StatusID = "REL", StatusName = "Sắp Công chiếu", CreatedDate = DateTime.Now });
            context.Statuss.Add(new Status { StatusID = "REW", StatusName = "Suất chiếu đặc biệt", CreatedDate = DateTime.Now });

        }

        public static void GenerateAccountType(MegaCinemaDBContext context)
        {
            context.AccountTypes.Add(new AccountType { TypeName = "Thành viên Mới", TypePoint = 0, TypeDiscount = 0, CreatedDate = DateTime.Now });
            context.AccountTypes.Add(new AccountType { TypeName = "Thành viên Bạc", TypePoint = 100, TypeDiscount = 5, CreatedDate = DateTime.Now });
            context.AccountTypes.Add(new AccountType { TypeName = "Thành viên Vip", TypePoint = 300, TypeDiscount = 10, CreatedDate = DateTime.Now });
        }

        public static void GenerateFilmCategories(MegaCinemaDBContext context)
        {
            context.FilmCategories.Add(new FilmCategory { FilmCategoryName = "Tình cảm", FilmCategoryDescrip = "không", CreatedDate = DateTime.Now });
            context.FilmCategories.Add(new FilmCategory { FilmCategoryName = "Hành động", FilmCategoryDescrip = "không", CreatedDate = DateTime.Now });
            context.FilmCategories.Add(new FilmCategory { FilmCategoryName = "Kinh dị", FilmCategoryDescrip = "không", CreatedDate = DateTime.Now });
            context.FilmCategories.Add(new FilmCategory { FilmCategoryName = "Viễn tưởng", FilmCategoryDescrip = "không", CreatedDate = DateTime.Now });
            context.FilmCategories.Add(new FilmCategory { FilmCategoryName = "Hài", FilmCategoryDescrip = "không", CreatedDate = DateTime.Now });
            context.FilmCategories.Add(new FilmCategory { FilmCategoryName = "Hoạt hình", FilmCategoryDescrip = "không", CreatedDate = DateTime.Now });
        }

        public static void GenerateFilmFormat(MegaCinemaDBContext context)
        {
            context.FilmFormats.Add(new FilmFormat { FilmFormatName = "2D", FilmFormatDescrip = "Định dạng sống động, chân thực", CreatedDate = DateTime.Now });
            context.FilmFormats.Add(new FilmFormat { FilmFormatName = "3D", FilmFormatDescrip = "Định dạng chân thực từng cảm xúc", CreatedDate = DateTime.Now });
            context.FilmFormats.Add(new FilmFormat { FilmFormatName = "IMAX", FilmFormatDescrip = "Hệ thống đầu tiên tại việt nam", CreatedDate = DateTime.Now });
        }

        public static void GenerateFilmRating(MegaCinemaDBContext context)
        {
            context.FilmRatings.Add(new FilmRating { RatingName = "Mới", CreatedDate = DateTime.Now });
            context.FilmRatings.Add(new FilmRating { RatingName = "Chưa công chiếu", CreatedDate = DateTime.Now });
            context.FilmRatings.Add(new FilmRating { RatingName = "Hot", CreatedDate = DateTime.Now });
        }

        public static void GenerateCinemaFeature(MegaCinemaDBContext context)
        {
        }

        public static void GenerateFoodList(MegaCinemaDBContext context)
        {
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iCouple Combo", FoodPrice = 127000, FoodDescription = "02 bắp vừa + 02 nước vừa + 01 snack", FoodPoster = "lovers.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iLovers' Combo", FoodPrice = 97000, FoodDescription = "01 bắp lớn + 02 nước vừa + 01 snack", FoodPoster = "mega.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iMEGA Combo", FoodPrice = 72000, FoodDescription = "01 bắp lớn + 01 nước vừa + 01 snack", FoodPoster = "mega-std.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iMEGA Standard Combo", FoodPrice = 60000, FoodDescription = "01 bắp lớn + 01 nước vừa", FoodPoster = "couple.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iSunday Combo", FoodPrice = 70000, FoodDescription = "01 hotdog + 01 nước vừa", FoodPoster = "sunday.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iCaramel Popcorn", FoodPrice = 45000, FoodDescription = "Bắp rang lớn", FoodPoster = "popcorn(1).png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iCheese Popcorn", FoodPrice = 45000, FoodDescription = "Bắp phô mai lớn", FoodPoster = "popcorn(2).png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iSweet Popcorn", FoodPrice = 40000, FoodDescription = "Bắp ngọt lớn", FoodPoster = "popcorn.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iAquafina", FoodPrice = 20000, FoodDescription = "Nước tinh khiết Aquafina 500ml", FoodPoster = "aquafina.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iBeverage", FoodPrice = 36000, FoodDescription = "Mirinda Cam Ly lớn", FoodPoster = "baverage.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iHotdog", FoodPrice = 40000, FoodDescription = "Hot dog", FoodPoster = "hotdog.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iNoodle Cup", FoodPrice = 22000, FoodDescription = "Mì ly", FoodPoster = "noodlecup.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iBeef Jerky", FoodPrice = 35000, FoodDescription = "Bò khô", FoodPoster = "khobo.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iBeer Heineken", FoodPrice = 40000, FoodDescription = "Bia Heinneken", FoodPoster = "heineken.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iChip", FoodPrice = 22000, FoodDescription = "Snack", FoodPoster = "chip.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iSeaweed", FoodPrice = 35000, FoodDescription = "Rong biển", FoodPoster = "seaweed.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iSnack", FoodPrice = 18000, FoodDescription = "Snack", FoodPoster = "snack.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iWall's Ice Cream Stick", FoodPrice = 15000, FoodDescription = "Kem que Wall's", FoodPoster = "wallsicecream.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iHaribo", FoodPrice = 50000, FoodDescription = "Kẹo Haribon (gói)", FoodPoster = "haribo.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iM&M", FoodPrice = 35000, FoodDescription = "Kẹo M&M gói", FoodPoster = "mm.png", FoodStatus = "AC", CreatedDate = DateTime.Now });
        }

        public static void GenerateSeatType(MegaCinemaDBContext context)
        {
            context.SeatTypes.Add(new SeatType { SeatTypeName = "Standard", SeatTypeSurcharge = 0, SeatTypeStatus = "AC", CreatedDate = DateTime.Now });
            context.SeatTypes.Add(new SeatType { SeatTypeName = "Vip", SeatTypeSurcharge = 5, SeatTypeStatus = "AC", CreatedDate = DateTime.Now });
            context.SeatTypes.Add(new SeatType { SeatTypeName = "Couple", SeatTypeSurcharge = 10, SeatTypeStatus = "AC", CreatedDate = DateTime.Now });
        }

        public static void GenerateTicketCategories(MegaCinemaDBContext context)
        {
            context.TicketCategories.Add(new TicketCategory { TicketCateName = "Vé thường", TicketCatePrice = 40000, TicketCateStatusID = "AC", CreatedDate = DateTime.Now });
            context.TicketCategories.Add(new TicketCategory { TicketCateName = "Vé Vip", TicketCatePrice = 60000, TicketCateStatusID = "AC", CreatedDate = DateTime.Now });
        }

        public static void GenerateFilm(MegaCinemaDBContext context)
        {
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "4 năm 2 chàng 1 tình yêu", FilmDuration = 100, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CJ", FilmTrailer = "https://www.youtube.com/watch?v=H4nuLncR2i8", FilmPoster = "FLM1.png", FilmRatingID = 2, FilmStatus = "PEN", CreatedDate = DateTime.Now });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Yêu em từ cái nhìn đầu tiên", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CJ", FilmTrailer = "https://www.youtube.com/watch?v=tI2IwWkp5ns", FilmPoster = "FLM2.png", FilmRatingID = 2, FilmStatus = "PEN", CreatedDate = DateTime.Now });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Xin lỗi anh chỉ là sát thủ", FilmDuration = 90, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CJ", FilmTrailer = "https://www.youtube.com/watch?v=6f3tpfQo6Y4", FilmPoster = "FLM3.png", FilmRatingID = 2, FilmStatus = "PEN", CreatedDate = DateTime.Now });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Căn phòng ám ảnh", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CJ", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM4.png", FilmRatingID = 2, FilmStatus = "REL", CreatedDate = DateTime.Now });

            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Cho em gần anh thêm chút nữa", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM5.jpg", FilmRatingID = 2, FilmStatus = "REL", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Chờ em đến ngày mai", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM6.jpg", FilmRatingID = 2, FilmStatus = "REL", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Bên nhau trọn đời", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM7.jpg", FilmRatingID = 2, FilmStatus = "REL", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "The avengers", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM8.jpg", FilmRatingID = 2, FilmStatus = "REL", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Avatar", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM9.jpg", FilmRatingID = 2, FilmStatus = "REW", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Quả tim máu", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM10.jpg", FilmRatingID = 2, FilmStatus = "REW", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Fan Cuồng", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM11.jpg", FilmRatingID = 2, FilmStatus = "REW", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Captain: Civil war", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM12.jpg", FilmRatingID = 2, FilmStatus = "REW", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Iron man", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM13.jpg", FilmRatingID = 2, FilmStatus = "PEN", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Điều tuyệt vời nhất của chúng ta", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CGV", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM14.jpg", FilmRatingID = 2, FilmStatus = "PEN", CreatedDate = DateTime.Now, FilmCategories = "Tình cảm, hài hước" });

        }

        public static void GenerateAdsBanner(MegaCinemaDBContext context)
        {
            context.AdsBanners.Add(new AdsBanner() { FilmId = 2, AdsDescription = "BNN1.png", CreatedDate = DateTime.Now });
            context.AdsBanners.Add(new AdsBanner() { FilmId = 2, AdsDescription = "BNN2.jpg", CreatedDate = DateTime.Now });
            context.AdsBanners.Add(new AdsBanner() { FilmId = 2, AdsDescription = "BNN3.png", CreatedDate = DateTime.Now });
            context.AdsBanners.Add(new AdsBanner() { FilmId = 2, AdsDescription = "BNN4.jpg", CreatedDate = DateTime.Now });
            context.AdsBanners.Add(new AdsBanner() { FilmId = 2, AdsDescription = "BNN5.png", CreatedDate = DateTime.Now });
        }

        public static void GenerateEventTopic(MegaCinemaDBContext context)
        {
            context.EventTopics.Add(new EventTopic() { EventTitle = "Cho em gần anh thêm chút nữa - phim ngôn tình", EventContent = "Bộ phim đạt doanh thu cao " });
            context.EventTopics.Add(new EventTopic() { EventTitle = "Chờ em đến ngày mai", EventContent = "Bộ phim đạt doanh thu cao " });
            context.EventTopics.Add(new EventTopic() { EventTitle = "Bên nhau trọn đời bản điện ảnh", EventContent = "Bộ phim đạt doanh thu cao " });
            context.EventTopics.Add(new EventTopic() { EventTitle = "Cho em gần anh thêm chút nữa - phim ngôn tình", EventContent = "Bộ phim đạt doanh thu cao " });

        }

        public static void GeneratePromotion(MegaCinemaDBContext context)
        {
            context.Promotions.Add(new Promotion()
            {
                PromotionHeader = "Mega day - Giáng sinh 2016",
                PromotionContent = "Nội dung ưu đãi",
                PromotionDateFinish = DateTime.Now,
                PromotionDateStart = DateTime.Now,
                PromotionPoster = "PRO1.jpg",
                PromotionStatusID = "AC"
            });

            context.Promotions.Add(new Promotion()
            {
                PromotionHeader = "Mega day - Black Friday",
                PromotionContent = "Nội dung ưu đãi",
                PromotionDateFinish = DateTime.Now,
                PromotionDateStart = DateTime.Now,
                PromotionPoster = "PRO2.jpg",
                PromotionStatusID = "AC"
            });

            context.Promotions.Add(new Promotion()
            {
                PromotionHeader = "Mega day - Thứ 4 kỳ diệu",
                PromotionContent = "Nội dung ưu đãi",
                PromotionDateFinish = DateTime.Now,
                PromotionDateStart = DateTime.Now,
                PromotionPoster = "PRO3.png",
                PromotionStatusID = "AC"
            });

            context.Promotions.Add(new Promotion()
            {
                PromotionHeader = "Mega day - Thứ 6 bạn bè",
                PromotionContent = "Nội dung ưu đãi",
                PromotionDateFinish = DateTime.Now,
                PromotionDateStart = DateTime.Now,
                PromotionPoster = "PRO4.jpg",
                PromotionStatusID = "AC"
            });

            context.Promotions.Add(new Promotion()
            {
                PromotionHeader = "Mega day - Valentine cùng Mega",
                PromotionContent = "Nội dung ưu đãi",
                PromotionDateFinish = DateTime.Now,
                PromotionDateStart = DateTime.Now,
                PromotionPoster = "PRO5.png",
                PromotionStatusID = "AC"
            });

        }

        public static void GenerateData(MegaCinemaDBContext context)
        {
            GenerateStatus(context);
            GenerateAccountType(context);
            GenerateFilmCategories(context);
            GenerateFilmFormat(context);
            GenerateFilmRating(context);
            GenerateFoodList(context);
            GenerateSeatType(context);
            GenerateTicketCategories(context);
            GenerateFilm(context);
        }

        public static void GenerateCinema(MegaCinemaDBContext context)
        {
            context.Cinemas.Add(new Cinema()
            {
                CinemaPrefix = "CNM",
                CinemaFullName = "Mega Cinema Cao thắng",
                CinemaAddress = "352 Cao Thắng, quận 3, Thành phố HCM",
                CinemaPhone = "19001879",
                CinemaManagerID = 4,
                CinemaEmail = "megagscinema@gmail.com",
                CinemaStatus = "AC",
                CreatedDate = DateTime.Now,
            });

            context.Cinemas.Add(new Cinema()
            {
                CinemaPrefix = "CNM",
                CinemaFullName = "Mega Cinema Nam kỳ khởi nghĩa",
                CinemaAddress = "155 Nam kỳ khởi nghĩa, TPHCM",
                CinemaPhone = "19001899",
                CinemaManagerID = 4,
                CinemaEmail = "megagscinema@gmail.com",
                CinemaStatus = "AC",
                CreatedDate = DateTime.Now,
            });
        }

        public static void GenerateFilmSession(MegaCinemaDBContext context)
        {
            context.FilmSessions.Add(new FilmSession()
            {
                FilmID = 2,
                CinemaID = 1,
                DateStartSession = DateTime.Now,
                DateFinishSession = DateTime.Now,
                FilmSessionStatusID = "AC",
                StaffID = 3,
            });

            context.FilmSessions.Add(new FilmSession()
            {
                FilmID = 3,
                CinemaID = 1,
                DateStartSession = DateTime.Now,
                DateFinishSession = DateTime.Now,
                FilmSessionStatusID = "AC",
                StaffID = 3,
            });

            context.FilmSessions.Add(new FilmSession()
            {
                FilmID = 4,
                CinemaID = 1,
                DateStartSession = DateTime.Now,
                DateFinishSession = DateTime.Now,
                FilmSessionStatusID = "AC",
                StaffID = 3,
            });

        }

        public static void GenerateTimeSession(MegaCinemaDBContext context)
        {
            for (int i = 0; i < 500; i++)
            {
                context.TimeSessions.Add(new TimeSession()
                {
                    TimeDetail = "none",
                    TimeStatus = "NOT",
                });
            }
        }

        public static string GenerateSeatState()
        {
            BookingPlan result = new BookingPlan();
            result.DateQuantity = 10;

            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "9:20",
                TimeBookingID = 1,
            });

            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "11:30",
                TimeBookingID = 2,
            });

            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "13:40",
                TimeBookingID = 3,
            });

            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "15:50",
                TimeBookingID = 4,
            });

            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "18:00",
                TimeBookingID = 5,
            });
            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "18:50",
                TimeBookingID = 6,
            });

            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "20:10",
                TimeBookingID = 7,
            });

            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "20:10",
                TimeBookingID = 8,
            });

            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "21:00",
                TimeBookingID = 9,
            });

            result.BookingTicketCalendar.Add(new BookingTime()
            {
                TimeBookingDetail = "22:20",
                TimeBookingID = 10,
            });

            BookingPlanTime res = new BookingPlanTime();
            res.BookingTicketTime.Add(result);

            int i = 1;
            foreach (var item in res.BookingTicketTime)
            {
                for (int j = 0; j < item.BookingTicketCalendar.Count; j++) 
                {
                    item.BookingTicketCalendar[j].TimeBookingID = i;
                    i++;
                }
            }

            return BookingTimeHelpers.ConvertCalendarToJson(res);
        }

        public static void GenerateFilmCalendarCreate(MegaCinemaDBContext context)
        {
            var result = GenerateSeatState();
            context.FilmCalendarCreates.Add(new FilmCalendarCreate
            {
                FilmSessionID = 3,
                StatusID = "AC",
                CreatedDate = DateTime.Now,
                FilmCalendarContent = result,
                StaffID = 3,
            });
        }
    }
}
