using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MegaCinemaModel.Models;
using MegaCinemaWeb.Models;

namespace MegaCinemaWeb.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateStatus(this Status status, StatusViewModel statusVm)
        {
            status.StatusID = statusVm.StatusID;
            status.StatusName = statusVm.StatusName;
            status.StatusDescription = statusVm.StatusDescription;
        }

        public static void UpdateFoodList(this FoodList foodList, FoodListViewModel foodListVm)
        {
            foodList.FoodPrefix = foodListVm.FoodPrefix;
            foodList.FoodName = foodListVm.FoodName;
            foodList.FoodPrice = foodListVm.FoodPrice;
            foodList.FoodDescription = foodListVm.FoodDescription;
            foodList.FoodPoster = foodListVm.FoodPoster;
            foodList.FoodStatus = foodListVm.FoodStatusID;
            foodList.CreatedDate = foodListVm.CreatedDate;
            foodList.CreatedBy = foodListVm.CreatedBy;
            foodList.UpdatedDate = foodListVm.UpdatedDate;
            foodList.UpdatedBy = foodListVm.UpdatedBy;
            foodList.MetaKeyword = foodListVm.MetaKeyword;
            foodList.MetaDescription = foodListVm.MetaDescription;
        }

        public static void UpdateFilmCategory(this FilmCategory filmCategory, FilmCategoryViewModel filmCategoryVm)
        {
            filmCategory.FilmCategoryName = filmCategoryVm.FilmCategoryName;
            filmCategory.FilmCategoryDescrip = filmCategoryVm.FilmCategoryDescrip;

            filmCategory.CreatedDate = filmCategoryVm.CreatedDate;
            filmCategory.CreatedBy = filmCategoryVm.CreatedBy;
            filmCategory.UpdatedDate = filmCategoryVm.UpdatedDate;
            filmCategory.UpdatedBy = filmCategoryVm.UpdatedBy;
            filmCategory.MetaKeyword = filmCategoryVm.MetaKeyword;
            filmCategory.MetaDescription = filmCategoryVm.MetaDescription;
        }

        public static void UpdateFilmFormat(this FilmFormat filmFormat, FilmFormatViewModel filmFormatVm)
        {
            filmFormat.FilmFormatName = filmFormatVm.FilmFormatName;
            filmFormat.FilmFormatDescrip = filmFormatVm.FilmFormatDescrip;

            filmFormat.CreatedDate = filmFormatVm.CreatedDate;
            filmFormat.CreatedBy = filmFormatVm.CreatedBy;
            filmFormat.UpdatedDate = filmFormatVm.UpdatedDate;
            filmFormat.UpdatedBy = filmFormatVm.UpdatedBy;
            filmFormat.MetaKeyword = filmFormatVm.MetaKeyword;
            filmFormat.MetaDescription = filmFormatVm.MetaDescription;
        }

        public static void UpdateFilm(this Film filmM, FilmViewModel filmVM)
        {
            filmM.FilmPrefix = filmVM.FilmPrefix;
            filmM.FilmName = filmVM.FilmName;
            filmM.FilmDuration = filmVM.FilmDuration;
            filmM.FilmFirstPremiered = filmVM.FilmFirstPremiered;
            filmM.FilmLanguage = filmVM.FilmLanguage;
            filmM.FilmContent = filmVM.FilmContent;
            filmM.FilmPoster = filmVM.FilmContent;
            filmM.FilmCompanyRelease = filmVM.FilmCompanyRelease;
            filmM.FilmTrailer = filmVM.FilmTrailer;
            filmM.FilmRatingID = filmVM.FilmRatingID;
            filmM.FilmStatus = filmVM.FilmStatus;
            filmM.CreatedDate = filmVM.CreatedDate;
            filmM.CreatedBy = filmVM.CreatedBy;
            filmM.UpdatedDate = filmVM.UpdatedDate;
            filmM.UpdatedBy = filmVM.UpdatedBy;
            filmM.MetaKeyword = filmVM.MetaKeyword;
            filmM.MetaDescription = filmVM.MetaDescription;
            filmM.FilmFinishPremiered = filmVM.FilmFinishPremiered;
        }

        public static void UpdatePromotion(this Promotion promotion, PromotionViewModel promotionVm)
        {
            promotion.PromotionHeader = promotionVm.PromotionHeader;
            promotion.PromotionContent = promotionVm.PromotionContent;
            promotion.PromotionPoster = promotionVm.PromotionPoster;
            promotion.PromotionDateStart = promotionVm.PromotionDateStart;
            promotion.PromotionDateFinish = promotionVm.PromotionDateFinish;
            promotion.PromotionStatusID = promotionVm.PromotionStatusID;

            promotion.CreatedDate = promotionVm.CreatedDate;
            promotion.CreatedBy = promotionVm.CreatedBy;
            promotion.UpdatedDate = promotionVm.UpdatedDate;
            promotion.UpdatedBy = promotionVm.UpdatedBy;
            promotion.MetaKeyword = promotionVm.MetaKeyword;
            promotion.MetaDescription = promotionVm.MetaDescription;

        }

        public static void UpdateCinemaFeature(this CinemaFeature cinemaFeature, CinemaFeatureViewModel cinemaFeatureViewModel)
        {

            //cinemaFeature.FeatureID = cinemaFeatureViewModel.FeatureID;
            cinemaFeature.FeatureType = cinemaFeatureViewModel.FeatureType;
            cinemaFeature.FeatureContent = cinemaFeatureViewModel.FeatureContent;
            cinemaFeature.FeatureDescription = cinemaFeatureViewModel.FeatureDescription;

            cinemaFeature.CreatedDate = cinemaFeatureViewModel.CreatedDate;
            cinemaFeature.CreatedBy = cinemaFeatureViewModel.CreatedBy;
            cinemaFeature.UpdatedDate = cinemaFeatureViewModel.UpdatedDate;
            cinemaFeature.UpdatedBy = cinemaFeatureViewModel.UpdatedBy;
            cinemaFeature.MetaKeyword = cinemaFeatureViewModel.MetaKeyword;
            cinemaFeature.MetaDescription = cinemaFeatureViewModel.MetaDescription;
        }

        public static void UpdateCinema(this Cinema cinema, CinemaViewModel cinemaViewModel)
        {

            //cinema.CinemaID = cinemaViewModel.CinemaID;
            cinema.CinemaPrefix = cinemaViewModel.CinemaPrefix;
            cinema.CinemaFullName = cinemaViewModel.CinemaFullName;
            cinema.CinemaAddress = cinemaViewModel.CinemaAddress;
            cinema.CinemaPhone = cinemaViewModel.CinemaPhone;
            cinema.CinemaEmail = cinemaViewModel.CinemaEmail;
            cinema.CinemaManagerID = cinemaViewModel.CinemaManagerID;
            cinema.CinemaStatus = cinemaViewModel.CinemaStatus;

            cinema.CreatedDate = cinemaViewModel.CreatedDate;
            cinema.CreatedBy = cinemaViewModel.CreatedBy;
            cinema.UpdatedDate = cinemaViewModel.UpdatedDate;
            cinema.UpdatedBy = cinemaViewModel.UpdatedBy;
            cinema.MetaKeyword = cinemaViewModel.MetaKeyword;
            cinema.MetaDescription = cinemaViewModel.MetaDescription;
        }

    }
}