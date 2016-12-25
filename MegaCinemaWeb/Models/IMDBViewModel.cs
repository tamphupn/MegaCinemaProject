using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaCinemaWeb.Models
{
    public class IMDBViewModel
    {
        public string MovieName { get; set; }
        public string MovieLink { get; set; }
        public string MovieLinkTrailer { get; set; }
        public string MovieLinkPoster { get; set; }
        public int MovieDuration { get; set; }
        public string MovieReleaseDate { get; set; }
        public long MovieBudget { get; set; }
        public string MovieGenre { get; set; }
        public string MovieStarActor { get; set; }
        public string MovieDirector { get; set; }
        public string MovieMusicby { get; set; }
        public string MovieProduction { get; set; }
        public string MovieWriter { get; set; }
    }
}