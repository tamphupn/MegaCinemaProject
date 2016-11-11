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
    }
}