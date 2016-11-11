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
        }
    }
}