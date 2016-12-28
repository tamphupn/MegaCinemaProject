using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaCinemaWeb.Models
{
    public class TicketDetailViewModel
    {
        public int? Adult { get; set; }
        public int? AdultVip { get; set; }
    }

    public class TicketSubmitDetail
    {
        public int index { get; set; }
        public string id { get; set; }
        public string nameSeat { get; set; }
        public string xLocation { get; set; }
        public string yLocation { get; set; }
        public string type { get; set; }
    }
}