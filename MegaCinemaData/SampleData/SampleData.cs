using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaCinemaModel.Models;

namespace MegaCinemaData.SampleData
{
    public static class SampleData
    {
        public static void GenerateStatus(MegaCinemaDBContext context)
        {
            context.Statuss.Add(new Status { StatusID = "NOT", StatusName = "Không hoạt động", CreatedDate = DateTime.Now });
            context.Statuss.Add(new Status { StatusID = "ACT", StatusName = "Đã kích hoạt", CreatedDate = DateTime.Now });
            context.Statuss.Add(new Status { StatusID = "NAT", StatusName = "Chưa kích hoạt", CreatedDate = DateTime.Now });
            context.Statuss.Add(new Status { StatusID = "AC", StatusName = "Đang hoạt động", CreatedDate = DateTime.Now });
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
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iCouple Combo", FoodPrice= 127000, FoodDescription = "02 bắp vừa + 02 nước vừa + 01 snack", FoodStatus = "AC",CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iLovers' Combo", FoodPrice = 97000, FoodDescription = "01 bắp lớn + 02 nước vừa + 01 snack", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iMEGA Combo", FoodPrice = 72000, FoodDescription = "01 bắp lớn + 01 nước vừa + 01 snack", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iMEGA Standard Combo", FoodPrice = 60000, FoodDescription = "01 bắp lớn + 01 nước vừa", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iSunday Combo", FoodPrice = 70000, FoodDescription = "01 hotdog + 01 nước vừa", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iCaramel Popcorn", FoodPrice = 45000, FoodDescription = "Bắp rang lớn", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iCheese Popcorn", FoodPrice = 45000, FoodDescription = "Bắp phô mai lớn", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iSweet Popcorn", FoodPrice = 40000, FoodDescription = "Bắp ngọt lớn", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iAquafina", FoodPrice = 20000, FoodDescription = "Nước tinh khiết Aquafina 500ml", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iBeverage", FoodPrice = 36000, FoodDescription = "Mirinda Cam Ly lớn", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iHotdog", FoodPrice = 40000, FoodDescription = "Hot dog", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iNoodle Cup", FoodPrice = 22000, FoodDescription = "Mì ly", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iBeef Jerky", FoodPrice = 35000, FoodDescription = "Bò khô", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iBeer Heineken", FoodPrice = 40000, FoodDescription = "Bia Heinneken", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iChip", FoodPrice = 22000, FoodDescription = "Snack", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iSeaweed", FoodPrice = 35000, FoodDescription = "Rong biển", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iSnack", FoodPrice = 18000, FoodDescription = "Snack", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iWall's Ice Cream Stick", FoodPrice = 15000, FoodDescription = "Kem que Wall's", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iHaribo", FoodPrice = 50000, FoodDescription = "Kẹo Haribon (gói)", FoodStatus = "AC", CreatedDate = DateTime.Now });
            context.FoodLists.Add(new FoodList { FoodPrefix = "FOO", FoodName = "iM&M", FoodPrice = 35000, FoodDescription = "Kẹo M&M gói", FoodStatus = "AC", CreatedDate = DateTime.Now });
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
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "4 năm 2 chàng 1 tình yêu", FilmDuration = 100, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CJ", FilmTrailer = "https://www.youtube.com/watch?v=H4nuLncR2i8", FilmPoster = "FLM1.png",FilmRatingID = 2, FilmStatus = "AC", CreatedDate = DateTime.Now});
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Yêu em từ cái nhìn đầu tiên", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CJ", FilmTrailer = "https://www.youtube.com/watch?v=tI2IwWkp5ns", FilmPoster = "FLM2.png", FilmRatingID = 2, FilmStatus = "AC", CreatedDate = DateTime.Now });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Xin lỗi anh chỉ là sát thủ", FilmDuration = 90, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CJ", FilmTrailer = "https://www.youtube.com/watch?v=6f3tpfQo6Y4", FilmPoster = "FLM3.png", FilmRatingID = 2, FilmStatus = "AC", CreatedDate = DateTime.Now });
            context.Films.Add(new Film { FilmPrefix = "FLM", FilmName = "Căn phòng ám ảnh", FilmDuration = 120, FilmFirstPremiered = DateTime.Now, FilmLanguage = "Phụ đề tiếng việt", FilmContent = "a", FilmCompanyRelease = "CJ", FilmTrailer = "https://www.youtube.com/watch?v=jvAVqAEu8ZM", FilmPoster = "FLM4.png", FilmRatingID = 2, FilmStatus = "AC", CreatedDate = DateTime.Now });
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
    }
}
