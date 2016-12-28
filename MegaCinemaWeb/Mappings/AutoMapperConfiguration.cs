using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MegaCinemaModel.Models;
using MegaCinemaWeb.Models;

namespace MegaCinemaWeb.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Status, StatusViewModel>();
            Mapper.CreateMap<FoodList, FoodListViewModel>();
            Mapper.CreateMap<Film, FilmViewModel>();
            Mapper.CreateMap<FilmCategory, FilmCategoryViewModel>();
            Mapper.CreateMap<FilmFormat, FilmFormatViewModel>();
			Mapper.CreateMap<Cinema, CinemaViewModel>();
            Mapper.CreateMap<CinemaFeature, CinemaFeatureViewModel>();
			Mapper.CreateMap<Promotion, PromotionViewModel>();
            Mapper.CreateMap<EventTopic, EventTopicViewModel>();
            Mapper.CreateMap<AdsBanner, AdsBannerViewModel>();
            Mapper.CreateMap<FilmCalendarCreate, FilmCalendarCreateViewModel>();
            Mapper.CreateMap<TimeSession, TimeSessionViewModel>();
            Mapper.CreateMap<PromotionCine, PromotionCineViewModel>();
        }
    }
}