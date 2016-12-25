using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCinemaCommon.DataCrawler
{
    public class IMDBModel
    {
        public string MovieName { get; set; }
        public string MovieLink { get; set; }
        public string MovieLinkTrailer { get; set; }
        public string MovieLinkPoster { get; set; }
        public int MovieDuration { get; set; }
        public string MovieReleaseDate { get; set; }
        public long MovieBudget { get; set; }
        public string MovieBudgetType { get; set; }
        public long MovieGrossUSA { get; set; }
        public string MovieGrossUSAType { get; set; }
        public long MovieGrossWorldwide { get; set; }
        public string MovieGrossWorldwideType { get; set; }
        public List<string> MovieGenre { get; set; }
        public List<string> MovieStarActor { get; set; }
        public List<string> MovieStarActorText { get; set; }
        public List<string> MovieDirector { get; set; }
        public List<string> MovieDirectorText { get; set; }
        public List<string> MovieMusicby { get; set; }
        public List<string> MovieMusicbyText { get; set; }
        public List<string> MovieProduction { get; set; }
        public List<string> MovieProductionText { get; set; }
        public List<string> MovieWriter { get; set; }
        public List<string> MovieWriterText { get; set; }
    }
}
