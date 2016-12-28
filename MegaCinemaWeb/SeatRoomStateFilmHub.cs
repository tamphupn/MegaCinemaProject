using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MegaCinemaWeb
{
    public class SeatRoomStateFilmHub : Hub
    {
        public static void UpdateSeatState()
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<SeatRoomStateFilmHub>();
            hubContext.Clients.All.UpdateSeatState();
        }
    }
}