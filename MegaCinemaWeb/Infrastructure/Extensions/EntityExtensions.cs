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
    }
}