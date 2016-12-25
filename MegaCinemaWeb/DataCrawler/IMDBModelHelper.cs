using HtmlAgilityPack;
using MegaCinemaCommon.ANNModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MegaCinemaCommon.ANNModel;
using System.Web;

namespace MegaCinemaCommon.DataCrawler
{
    public class IMDBModelHelper
    {
        public enum FilmState
        {
            FLOP = 0,
            HIT = 1,
            SUPERHIT = 2,
            DEFAULT = 3,
        }
        public enum TypeData
        {
            TRAIN = 0,
            PREDICTION = 1,
        }

        private const string MOVIE_LINK_POSTER_NONE = "@none";
        private const string MOVIE_LINK_TRAILER_NONE = "@none";
        private const int MOVIE_DURATION_NONE = 0;
        private const long MOVIE_BUDGET_NONE = 0;
        private const string MOVIE_BUDGET_TYPE_NONE = "@none";
        private const string MOVIE_RELEASE_DATE_NONE = "@none";

        public static IMDBModel GetMovieInformation(string movieName)
        {
            WebClient client = new WebClient();

            // 1. Kiem tra neu page search khong co ket qua thi return null
            HtmlDocument htmlDocSearch = new HtmlDocument();
            string htmlSourceSearch;
            string movieNameForSearch = movieName.Replace(" ", @"%20");
            string searchUrl = string.Format(@"http://www.imdb.com/find?q={0}&s=tt&ttype=ft", movieNameForSearch);
            htmlSourceSearch = DownloadHtmlSource(client, searchUrl);
            htmlDocSearch.LoadHtml(htmlSourceSearch);
            HtmlNode node = htmlDocSearch.DocumentNode.SelectSingleNode("//*[@id='main']/div/div/h3/a[@name='tt']");
            if (node == null)
                return null;


            // 2. Lay cac ket qua va gan cho 'result'
            IMDBModel result = new IMDBModel();

            // a. Lay MovieName, MovieLink
            HtmlNode nodeA = htmlDocSearch.DocumentNode.SelectSingleNode("//*[@id='main']/div/div[2]/table[@class='findList']/tbody/tr/td[@class='result_text']/a");
            if (nodeA == null) nodeA = htmlDocSearch.DocumentNode.SelectSingleNode("//*[@id='main']/div/div[2]/table[@class='findList']/tr/td[@class='result_text']/a");
            result.MovieName = nodeA.InnerText.Trim();
            result.MovieLink = @"http://www.imdb.com" + nodeA.GetAttributeValue("href", null).Split('?')[0];


            HtmlDocument htmlDocMovie = new HtmlDocument();
            string htmlSourceMovie;
            htmlSourceMovie = DownloadHtmlSource(client, result.MovieLink);
            htmlDocMovie.LoadHtml(htmlSourceMovie);

            // b. Lay MovieLinkPoster, MovieLinkTrailer, MovieDuration, MovieReleaseDate, MovieBudget, MovieBudgetType
            #region
            HtmlNode nodeB;

            // lay poster link
            nodeB = htmlDocMovie.DocumentNode.SelectSingleNode("//*[@id='title-overview-widget']/div/div[@class='poster']/a/img");
            if (nodeB == null) nodeB = htmlDocMovie.DocumentNode.SelectSingleNode("//*[@id='title-overview-widget']/div/div/div[@class='poster']/a/img");
            if (nodeB == null) result.MovieLinkPoster = MOVIE_LINK_POSTER_NONE;
            //else result.MovieLinkPoster = @"http://www.imdb.com" + nodeB.GetAttributeValue("href", null).Split('?')[0];
            else result.MovieLinkPoster = nodeB.GetAttributeValue("src", MOVIE_LINK_POSTER_NONE);

            // lay trailer
            nodeB = htmlDocMovie.DocumentNode.SelectSingleNode("//*[@id='title-overview-widget']/div/div/div[@class='slate']/a");
            if (nodeB == null) result.MovieLinkTrailer = MOVIE_LINK_TRAILER_NONE;
            else
            {
                result.MovieLinkTrailer = @"http://www.imdb.com" + nodeB.GetAttributeValue("href", MOVIE_LINK_TRAILER_NONE).Split('?')[0];
                if (result.MovieLinkTrailer != MOVIE_LINK_TRAILER_NONE)
                    result.MovieLinkTrailer = string.Format(@"http://www.imdb.com/video/imdb/{0}/imdb/embed?autoplay=false&width=480", Regex.Match(result.MovieLinkTrailer, @"vi\d+").Value);
            }

            // lay duration
            nodeB = htmlDocMovie.DocumentNode.SelectSingleNode("//*[@id='title-overview-widget']/div/div/div/div/div[@class='title_wrapper']/div/time[@itemprop='duration']");
            if (nodeB == null) result.MovieDuration = MOVIE_DURATION_NONE;
            else result.MovieDuration = int.Parse(Regex.Match(nodeB.GetAttributeValue("datetime", null), @"\d+").Value);

            // lay release date
            //*[@id="title-overview-widget"]/div/div/div/div/div[@class='title_wrapper']/div/a
            nodeB = htmlDocMovie.DocumentNode.SelectSingleNode("//*[@id='title-overview-widget']/div/div/div/div/div[@class='title_wrapper']/div/a/meta[@itemprop='datePublished']");
            if (nodeB == null) result.MovieReleaseDate = MOVIE_RELEASE_DATE_NONE;
            else result.MovieReleaseDate = nodeB.GetAttributeValue("content", null);

            // lay budget, budget type
            nodeB = htmlDocMovie.DocumentNode.SelectSingleNode("//*[@id='titleDetails']/div[./h4 = 'Budget:']");
            if (nodeB == null) { result.MovieBudget = MOVIE_BUDGET_NONE; result.MovieBudgetType = MOVIE_BUDGET_TYPE_NONE; }
            else
            {
                string budget = WebUtility.HtmlDecode(nodeB.InnerText);
                budget = Regex.Match(budget, @".[\d+,]+").Value;
                result.MovieBudgetType = "" + budget[0];
                result.MovieBudget = long.Parse(budget.Remove(0, 1).Replace(",", ""));
            }

            #endregion

            // c. Lay MovieGenre
            #region
            result.MovieGenre = new List<string>();
            HtmlNodeCollection nodeC = htmlDocMovie.DocumentNode.SelectNodes("//*[@id='title-overview-widget']/div/div/div/div/div[@class='title_wrapper']/div/a/span[@itemprop='genre']");
            if (nodeC != null)
            {
                foreach (HtmlNode nodeChild in nodeC)
                    result.MovieGenre.Add(nodeChild.InnerText.Trim());
            }
            #endregion

            // d. Lay MovieStarActor
            #region
            result.MovieStarActor = new List<string>();
            result.MovieStarActorText = new List<string>();
            HtmlNodeCollection nodeD = htmlDocMovie.DocumentNode.SelectNodes("//*[@id='title-overview-widget']/div/div[@class='plot_summary_wrapper']/div/div[./h4='Stars:']/span[@itemprop='actors']/a");
            if (nodeD == null) nodeD = htmlDocMovie.DocumentNode.SelectNodes("//*[@id='title-overview-widget']/div[@class='plot_summary_wrapper']/div/div[./h4='Stars:']/span[@itemprop='actors']/a");
            if (nodeD != null)
            {
                foreach (HtmlNode nodeChild in nodeD)
                {
                    result.MovieStarActor.Add(nodeChild.SelectSingleNode("./span").InnerText.Trim());
                    result.MovieStarActorText.Add(Regex.Match(nodeChild.GetAttributeValue("href", null), @"nm\d+").Value);
                }
            }
            #endregion

            // e. Lay MovieDirector
            #region
            result.MovieDirector = new List<string>();
            result.MovieDirectorText = new List<string>();
            HtmlNodeCollection nodeE = htmlDocMovie.DocumentNode.SelectNodes("//*[@id='title-overview-widget']/div/div[@class='plot_summary_wrapper']/div/div[./h4='Director:']/span[@itemprop='director']/a");
            if (nodeE == null) nodeE = htmlDocMovie.DocumentNode.SelectNodes("//*[@id='title-overview-widget']/div[@class='plot_summary_wrapper']/div/div[./h4='Director:']/span[@itemprop='director']/a");
            if (nodeE == null) nodeE = htmlDocMovie.DocumentNode.SelectNodes("//*[@id='title-overview-widget']/div/div[@class='plot_summary_wrapper']/div/div[./h4='Directors:']/span[@itemprop='director']/a");
            if (nodeE == null) nodeE = htmlDocMovie.DocumentNode.SelectNodes("//*[@id='title-overview-widget']/div[@class='plot_summary_wrapper']/div/div[./h4='Directors:']/span[@itemprop='director']/a");
            if (nodeE != null)
            {
                foreach (HtmlNode nodeChild in nodeE)
                {
                    result.MovieDirector.Add(nodeChild.SelectSingleNode("./span").InnerText.Trim());
                    result.MovieDirectorText.Add(Regex.Match(nodeChild.GetAttributeValue("href", null), @"nm\d+").Value);
                }
            }
            #endregion

            HtmlDocument htmlDocProduction = new HtmlDocument();
            string htmlSourceProduction = DownloadHtmlSource(client, string.Format(result.MovieLink + "companycredits"));
            htmlDocProduction.LoadHtml(htmlSourceProduction);

            // f. Lay MovieProduction
            #region
            result.MovieProduction = new List<string>();
            result.MovieProductionText = new List<string>();
            HtmlNodeCollection nodeF = htmlDocProduction.DocumentNode.SelectNodes("//*[@id='company_credits_content']/ul[@class='simpleList' and preceding::*[@id='production']][1]/li/a");
            if (nodeE != null)
            {
                foreach (HtmlNode nodeChild in nodeF)
                {
                    result.MovieProduction.Add(nodeChild.InnerText.Trim());
                    result.MovieProductionText.Add(Regex.Match(nodeChild.GetAttributeValue("href", null), @"co\d+").Value);
                }
            }
            #endregion

            HtmlDocument htmlDocFullCredit = new HtmlDocument();
            string htmlSourceFullCredit = DownloadHtmlSource(client, string.Format(result.MovieLink + "fullcredits"));
            htmlDocFullCredit.LoadHtml(htmlSourceFullCredit);

            // g. Lay MovieMusicBy
            #region
            result.MovieMusicby = new List<string>();
            result.MovieMusicbyText = new List<string>();
            HtmlNode nodeCheckMusicBy = htmlDocFullCredit.DocumentNode.SelectSingleNode("//*[@id='fullcredits_content']/table[preceding::h4[text()='Music by&nbsp;']]");
            if (nodeCheckMusicBy != null)
            {
                HtmlNodeCollection nodeG = nodeCheckMusicBy.SelectNodes("./tbody/tr");
                if (nodeG == null) nodeG = nodeCheckMusicBy.SelectNodes("./tr");
                if (nodeG != null)
                {
                    foreach (HtmlNode nodeChild in nodeG)
                    {
                        HtmlNode nodeMusicBy = nodeChild.SelectSingleNode("./td/a");
                        result.MovieMusicby.Add(nodeMusicBy.InnerText.Trim());
                        result.MovieMusicbyText.Add(Regex.Match(nodeMusicBy.GetAttributeValue("href", null), @"nm\d+").Value);
                    }
                }
            }
            #endregion

            // h. Lay MovieWriter
            #region
            result.MovieWriter = new List<string>();
            result.MovieWriterText = new List<string>();
            bool foundWriter = false;
            foreach (HtmlNode nodeChild in htmlDocFullCredit.DocumentNode.SelectNodes("//div[@id='fullcredits_content']/*"))
            {
                if (foundWriter)
                {
                    HtmlNodeCollection nodeH = nodeChild.SelectNodes("./tbody/tr");
                    if (nodeH == null) nodeH = nodeChild.SelectNodes("./tr");
                    foreach (HtmlNode nodeWriter in nodeH)
                    {
                        HtmlNode nodeGotWriter = nodeWriter.SelectSingleNode("./td[@class='name']/a");
                        if (nodeGotWriter == null) continue;
                        string writerId = Regex.Match(nodeGotWriter.GetAttributeValue("href", null), @"nm\d+").Value;
                        if (result.MovieWriterText.Contains(writerId))
                            continue;

                        result.MovieWriterText.Add(writerId);
                        result.MovieWriter.Add(nodeGotWriter.InnerText.Trim());
                    }
                    break;
                }
                if (nodeChild.InnerText.Contains("Writing Credits"))
                    foundWriter = true;
            }
            #endregion

            return result;
        }

        private static string DownloadHtmlSource(WebClient client, string url)
        {
            while (true)
            {
                try
                {
                    return client.DownloadString(url);
                }
                catch (System.Net.WebException)
                {
                    Console.WriteLine("#WebException. Trying to download again...");
                    continue;
                }
            }
        }

        public static double GetActorValue(string idActor, DateTime dateCheck)
        {
            //Nhận id diễn viên, lấy danh phim theo các phim mà diễn viên đó đã đóng - nội dung lấy bao gồm:
            //Chi phí sản xuất, ID phim, tên phim, lấy doanh thu và chi phí sản xuất để phân loại phim
            //Sắp xếp các phim theo thời gian công chiếu tăng dần
            //Lấy tất cả các phim của kể từ ngày dateCheck trở về trước, lấy 5 phim người đó đóng gần nhất kể từ
            //thời điểm datecheck, lấy số phim từ hit trở lên(bao gồm hit và superhit) chia cho 5
            //nếu chưa đủ 5 thì lấy tất cả chia cho số lượng đó
            //ghi ra file theo ID diễn viên đó - Format bao gồm
            //ID Phim - Tên Phim - Budget - Doanh Thu -  Ngày công chiếu - Đánh giá phim HIt hay Super hay flop

            /**
             Hàm trả về:
                -1 : nếu trước datecheck ko có phim nào có diễn viên idActor
                -2 : nếu ko tìm thấy file idActor.txt
             */
            double result;
            const int RETURN_NO_MOVIE_BEFORE_DATECHECK = -1;
            const int RETURN_FILE_NOT_FOUND = -2;
            const string DIR_DATA_NAME = "Task1";
            List<DateTime> listGotDate = new List<DateTime>();
            List<long> listGotBudget = new List<long>();
            List<long> listGotGrossWorldwide = new List<long>();
            List<long> listGotGrossUSA = new List<long>();
            int[] arrIndexTop5;
            List<FilmState> listTop5MovieState = new List<FilmState>();

            // 1. Lấy top 5 phim gần nhất trước ngày dateCheck

            // 1.1. Lấy các phim trước ngày dateCheck
            #region
            // Đọc file data từ file
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/FilmPreditionData/Task1/" + idActor + ".txt");
            if (File.Exists(filePath) == false)
                return RETURN_FILE_NOT_FOUND;

            // Duyệt từng dòng, kiểm tra các đkiện của phim: date khác none, date < dateCheck, phim có budget && có gross (worldwide || usa)
            var linesRead = File.ReadLines(filePath);
            foreach (string line in linesRead)
            {
                string[] values = line.Split('\t');

                // Kiem tra ngay phat hanh khac @none
                if (values[3] == MOVIE_RELEASE_DATE_NONE)
                    continue;

                // Lay ngay phay hanh
                DateTime dateGot;
                string[] date = values[3].Split('-');
                if (date.Length - 1 == 1)
                    dateGot = new DateTime(int.Parse(date[0]), int.Parse(date[1]), 1);
                else if (date.Length - 1 == 0)
                    dateGot = new DateTime(int.Parse(date[0]), 1, 1);
                else
                    dateGot = DateTime.ParseExact(values[3], "yyyy-MM-dd", null);

                // Kiem tra ngay phat hanh < dateCheck
                if (dateGot < dateCheck)
                {
                    // Lay budget
                    long budgetGot = (values[4].Length > 1 ? long.Parse(values[4].Remove(0, 1)) : long.Parse(values[4]));
                    if (budgetGot == 0)
                        continue;

                    // Lay gross worldwide, usa
                    long grossWorldwideGot = (values[5].Length > 1 ? long.Parse(values[5].Remove(0, 1)) : long.Parse(values[5]));
                    long grossUSAGot = (values[6].Length > 1 ? long.Parse(values[6].Remove(0, 1)) : long.Parse(values[6]));

                    // Neu ton tai gross thi them vao list
                    if (grossWorldwideGot != 0 || grossUSAGot != 0)
                    {
                        listGotDate.Add(dateGot);
                        listGotBudget.Add(budgetGot);
                        listGotGrossWorldwide.Add(grossWorldwideGot);
                        listGotGrossUSA.Add(grossUSAGot);
                    }
                }
            }
            #endregion

            // 1.2. Nếu trước ngày datecheck ko có phần tử nào thì trả về RETURN_NO_MOVIE_BEFORE_DATECHECK
            if (listGotDate.Count == 0)
                return RETURN_NO_MOVIE_BEFORE_DATECHECK;

            // 1.3. Lọc lại list và chỉ lấy top 5 phim
            #region
            // Khoi tao array index arrIndex tu 0 -> listGotDate.count - 1;
            arrIndexTop5 = new int[listGotDate.Count];
            for (int _i = 0; _i < arrIndexTop5.Length; _i++)
                arrIndexTop5[_i] = _i;

            if (listGotDate.Count > 5)
            {
                // Sort listGotDate theo index cua arrIndex, chi 5 phan tu dau thi dung lai
                int i, j;
                for (j = 0; j < 5; j++)
                {
                    int iMin = j;
                    for (i = j + 1; i < arrIndexTop5.Length; i++)
                    {
                        if (listGotDate[arrIndexTop5[i]] < listGotDate[arrIndexTop5[iMin]])
                        {
                            iMin = i;
                        }
                    }

                    if (iMin != j)
                    {
                        int temp = arrIndexTop5[j];
                        arrIndexTop5[j] = arrIndexTop5[iMin];
                        arrIndexTop5[iMin] = temp;
                    }
                }
            }
            #endregion

            // 2. Tính film state các phim trong top 5
            int countTopFilm = (listGotDate.Count > 5 ? 5 : listGotDate.Count);
            for (int i = 0; i < countTopFilm; i++)
            {
                long gross = (listGotGrossWorldwide[arrIndexTop5[i]] != 0 ? listGotGrossWorldwide[arrIndexTop5[i]] : listGotGrossUSA[arrIndexTop5[i]]);
                long budget = listGotBudget[arrIndexTop5[i]];
                FilmState fs = FilmClassification(budget, gross);
                listTop5MovieState.Add(fs);
            }

            // 3. Tính kết quả
            int countHitSuperHit = 0;
            foreach (FilmState fs in listTop5MovieState)
            {
                if (fs == FilmState.HIT || fs == FilmState.SUPERHIT)
                    countHitSuperHit++;
            }
            result = (double)countHitSuperHit / (double)listTop5MovieState.Count;

            return result;
        }

        public static double GetDirectorValue(string idDirector, DateTime dateCheck)
        {
            /**
             Hàm trả về:
                -1 : nếu trước datecheck ko có phim nào có đạo diễn idDirector
                -2 : nếu ko tìm thấy các file data của task 2
             */
            double result;
            const int RETURN_NO_MOVIE_BEFORE_DATECHECK = -1;
            const int RETURN_FILE_NOT_FOUND = -2;
            const string DIR_DATA_NAME = "Task2";
            List<DateTime> listGotDate = new List<DateTime>();
            List<long> listGotBudget = new List<long>();
            List<long> listGotGrossWorldwide = new List<long>();
            List<long> listGotGrossUSA = new List<long>();
            int[] arrIndexTop5;
            List<FilmState> listTop5MovieState = new List<FilmState>();

            // 1. Lấy top 5 phim gần nhất trước ngày dateCheck

            // 1.1. Lấy các phim trước ngày dateCheck
            #region
            // Kiểm tra có đầy đủ 25 part data của task 2 không
            List<string> filePaths = new List<string>();
            for (int part = 0; part < 25; part++)
            {
                filePaths.Add(System.Web.HttpContext.Current.Server.MapPath("~/FilmPreditionData/Task2/task2.part" + part + ".txt"));
                if (File.Exists(filePaths[part]) == false)
                    return RETURN_FILE_NOT_FOUND;
            }

            // Duyệt từng dòng, kiểm tra các đkiện của phim: date khác none, date < dateCheck, phim có budget && có gross (worldwide || usa)
            foreach (string filePath in filePaths)
            {
                var linesRead = File.ReadLines(filePath);
                foreach (string line in linesRead)
                {
                    string[] values = line.Split('\t');

                    // Kiem tra id dao dien (*)
                    if (values[2] != idDirector) continue;

                    // Kiem tra ngay phat hanh khac @none
                    if (values[3] == MOVIE_RELEASE_DATE_NONE)
                        continue;

                    // Lay ngay phay hanh
                    DateTime dateGot;
                    string[] date = values[3].Split('-');
                    if (date.Length - 1 == 1)
                        dateGot = new DateTime(int.Parse(date[0]), int.Parse(date[1]), 1);
                    else if (date.Length - 1 == 0)
                        dateGot = new DateTime(int.Parse(date[0]), 1, 1);
                    else
                        dateGot = DateTime.ParseExact(values[3], "yyyy-MM-dd", null);

                    // Kiem tra ngay phat hanh < dateCheck
                    if (dateGot < dateCheck)
                    {
                        // Lay budget
                        long budgetGot = (values[5].Length > 1 ? long.Parse(values[5].Remove(0, 1)) : long.Parse(values[5]));
                        if (budgetGot == 0)
                            continue;

                        // Lay gross worldwide, usa
                        long grossWorldwideGot = (values[6].Length > 1 ? long.Parse(values[6].Remove(0, 1)) : long.Parse(values[6]));
                        long grossUSAGot = (values[7].Length > 1 ? long.Parse(values[7].Remove(0, 1)) : long.Parse(values[7]));

                        // Neu ton tai gross thi them vao list
                        if (grossWorldwideGot != 0 || grossUSAGot != 0)
                        {
                            listGotDate.Add(dateGot);
                            listGotBudget.Add(budgetGot);
                            listGotGrossWorldwide.Add(grossWorldwideGot);
                            listGotGrossUSA.Add(grossUSAGot);
                        }
                    }
                }
            }
            #endregion

            // 1.2. Nếu trước ngày datecheck ko có phần tử nào thì trả về RETURN_NO_MOVIE_BEFORE_DATECHECK
            if (listGotDate.Count == 0)
                return RETURN_NO_MOVIE_BEFORE_DATECHECK;

            // 1.3. Lọc lại list và chỉ lấy top 5 phim
            #region
            // Khoi tao array index arrIndex tu 0 -> listGotDate.count - 1;
            arrIndexTop5 = new int[listGotDate.Count];
            for (int _i = 0; _i < arrIndexTop5.Length; _i++)
                arrIndexTop5[_i] = _i;

            if (listGotDate.Count > 5)
            {
                // Sort listGotDate theo index cua arrIndex, chi 5 phan tu dau thi dung lai
                int i, j;
                for (j = 0; j < 5; j++)
                {
                    int iMin = j;
                    for (i = j + 1; i < arrIndexTop5.Length; i++)
                    {
                        if (listGotDate[arrIndexTop5[i]] < listGotDate[arrIndexTop5[iMin]])
                        {
                            iMin = i;
                        }
                    }

                    if (iMin != j)
                    {
                        int temp = arrIndexTop5[j];
                        arrIndexTop5[j] = arrIndexTop5[iMin];
                        arrIndexTop5[iMin] = temp;
                    }
                }
            }
            #endregion

            // 2. Tính film state các phim trong top 5
            int countTopFilm = (listGotDate.Count > 5 ? 5 : listGotDate.Count);
            for (int i = 0; i < countTopFilm; i++)
            {
                long gross = (listGotGrossWorldwide[arrIndexTop5[i]] != 0 ? listGotGrossWorldwide[arrIndexTop5[i]] : listGotGrossUSA[arrIndexTop5[i]]);
                long budget = listGotBudget[arrIndexTop5[i]];
                FilmState fs = FilmClassification(budget, gross);
                listTop5MovieState.Add(fs);
            }

            // 3. Tính kết quả
            int countHitSuperHit = 0;
            foreach (FilmState fs in listTop5MovieState)
            {
                if (fs == FilmState.HIT || fs == FilmState.SUPERHIT)
                    countHitSuperHit++;
            }
            result = (double)countHitSuperHit / (double)listTop5MovieState.Count;

            return result;
        }

        public static double GetProductionCompanyValue(string idProductionCompany, DateTime dateCheck)
        {
            /**
            Hàm trả về:
               -1 : nếu trước datecheck ko có phim nào có công ty sản xuất là idProductionCompany
               -2 : nếu ko tìm thấy các file data của task 3
            */
            double result;
            const int RETURN_NO_MOVIE_BEFORE_DATECHECK = -1;
            const int RETURN_FILE_NOT_FOUND = -2;
            const string DIR_DATA_NAME = "Task3";
            List<DateTime> listGotDate = new List<DateTime>();
            List<long> listGotBudget = new List<long>();
            List<long> listGotGrossWorldwide = new List<long>();
            List<long> listGotGrossUSA = new List<long>();
            int[] arrIndexTop5;
            List<FilmState> listTop5MovieState = new List<FilmState>();

            // 1. Lấy top 5 phim gần nhất trước ngày dateCheck

            // 1.1. Lấy các phim trước ngày dateCheck
            #region
            // Kiểm tra có đầy đủ 25 part data của task 3 không
            List<string> filePaths = new List<string>();
            for (int part = 0; part < 25; part++)
            {
                filePaths.Add(System.Web.HttpContext.Current.Server.MapPath("~/FilmPreditionData/Task3/task3.part" + part + ".txt"));
                if (File.Exists(filePaths[part]) == false)
                    return RETURN_FILE_NOT_FOUND;
            }

            // Duyệt từng dòng, kiểm tra các đkiện của phim: date khác none, date < dateCheck, phim có budget && có gross (worldwide || usa)
            foreach (string filePath in filePaths)
            {
                var linesRead = File.ReadLines(filePath);
                foreach (string line in linesRead)
                {
                    string[] values = line.Split('\t');

                    // Kiem tra id dao dien (*)
                    if (values[2] != idProductionCompany) continue;

                    // Kiem tra ngay phat hanh khac @none
                    if (values[3] == MOVIE_RELEASE_DATE_NONE)
                        continue;

                    // Lay ngay phay hanh
                    DateTime dateGot;
                    string[] date = values[3].Split('-');
                    if (date.Length - 1 == 1)
                        dateGot = new DateTime(int.Parse(date[0]), int.Parse(date[1]), 1);
                    else if (date.Length - 1 == 0)
                        dateGot = new DateTime(int.Parse(date[0]), 1, 1);
                    else
                        dateGot = DateTime.ParseExact(values[3], "yyyy-MM-dd", null);

                    // Kiem tra ngay phat hanh < dateCheck
                    if (dateGot < dateCheck)
                    {
                        // Lay budget
                        long budgetGot = (values[5].Length > 1 ? long.Parse(values[5].Remove(0, 1)) : long.Parse(values[5]));
                        if (budgetGot == 0)
                            continue;

                        // Lay gross worldwide, usa
                        long grossWorldwideGot = (values[6].Length > 1 ? long.Parse(values[6].Remove(0, 1)) : long.Parse(values[6]));
                        long grossUSAGot = (values[7].Length > 1 ? long.Parse(values[7].Remove(0, 1)) : long.Parse(values[7]));

                        // Neu ton tai gross thi them vao list
                        if (grossWorldwideGot != 0 || grossUSAGot != 0)
                        {
                            listGotDate.Add(dateGot);
                            listGotBudget.Add(budgetGot);
                            listGotGrossWorldwide.Add(grossWorldwideGot);
                            listGotGrossUSA.Add(grossUSAGot);
                        }
                    }
                }
            }
            #endregion

            // 1.2. Nếu trước ngày datecheck ko có phần tử nào thì trả về RETURN_NO_MOVIE_BEFORE_DATECHECK
            if (listGotDate.Count == 0)
                return RETURN_NO_MOVIE_BEFORE_DATECHECK;

            // 1.3. Lọc lại list và chỉ lấy top 5 phim
            #region
            // Khoi tao array index arrIndex tu 0 -> listGotDate.count - 1;
            arrIndexTop5 = new int[listGotDate.Count];
            for (int _i = 0; _i < arrIndexTop5.Length; _i++)
                arrIndexTop5[_i] = _i;

            if (listGotDate.Count > 5)
            {
                // Sort listGotDate theo index cua arrIndex, chi 5 phan tu dau thi dung lai
                int i, j;
                for (j = 0; j < 5; j++)
                {
                    int iMin = j;
                    for (i = j + 1; i < arrIndexTop5.Length; i++)
                    {
                        if (listGotDate[arrIndexTop5[i]] < listGotDate[arrIndexTop5[iMin]])
                        {
                            iMin = i;
                        }
                    }

                    if (iMin != j)
                    {
                        int temp = arrIndexTop5[j];
                        arrIndexTop5[j] = arrIndexTop5[iMin];
                        arrIndexTop5[iMin] = temp;
                    }
                }
            }
            #endregion

            // 2. Tính film state các phim trong top 5
            int countTopFilm = (listGotDate.Count > 5 ? 5 : listGotDate.Count);
            for (int i = 0; i < countTopFilm; i++)
            {
                long gross = (listGotGrossWorldwide[arrIndexTop5[i]] != 0 ? listGotGrossWorldwide[arrIndexTop5[i]] : listGotGrossUSA[arrIndexTop5[i]]);
                long budget = listGotBudget[arrIndexTop5[i]];
                FilmState fs = FilmClassification(budget, gross);
                listTop5MovieState.Add(fs);
            }

            // 3. Tính kết quả
            int countHitSuperHit = 0;
            foreach (FilmState fs in listTop5MovieState)
            {
                if (fs == FilmState.HIT || fs == FilmState.SUPERHIT)
                    countHitSuperHit++;
            }
            result = (double)countHitSuperHit / (double)listTop5MovieState.Count;

            return result;
        }

        public static double GetProducerValue(string idProducer, DateTime dateCheck)

        {
            /**
            Hàm trả về:
               -1 : nếu trước datecheck ko có phim nào có người sản xuất là idProducer
               -2 : nếu ko tìm thấy các file data của task 4
            */
            double result;
            const int RETURN_NO_MOVIE_BEFORE_DATECHECK = -1;
            const int RETURN_FILE_NOT_FOUND = -2;
            const string DIR_DATA_NAME = "Task4";
            List<DateTime> listGotDate = new List<DateTime>();
            List<long> listGotBudget = new List<long>();
            List<long> listGotGrossWorldwide = new List<long>();
            List<long> listGotGrossUSA = new List<long>();
            int[] arrIndexTop5;
            List<FilmState> listTop5MovieState = new List<FilmState>();

            // 1. Lấy top 5 phim gần nhất trước ngày dateCheck

            // 1.1. Lấy các phim trước ngày dateCheck
            #region
            // Kiểm tra có đầy đủ 25 part data của task 4 không
            List<string> filePaths = new List<string>();
            for (int part = 0; part < 25; part++)
            {
                filePaths.Add(System.Web.HttpContext.Current.Server.MapPath("~/FilmPreditionData/Task4/task4.part" + part + ".txt"));
                if (File.Exists(filePaths[part]) == false)
                    return RETURN_FILE_NOT_FOUND;
            }

            // Duyệt từng dòng, kiểm tra các đkiện của phim: date khác none, date < dateCheck, phim có budget && có gross (worldwide || usa)
            foreach (string filePath in filePaths)
            {
                var linesRead = File.ReadLines(filePath);
                foreach (string line in linesRead)
                {
                    string[] values = line.Split('\t');

                    // Kiem tra id dao dien (*)
                    if (values[2] != idProducer) continue;

                    // Kiem tra ngay phat hanh khac @none
                    if (values[3] == MOVIE_RELEASE_DATE_NONE)
                        continue;

                    // Lay ngay phay hanh
                    DateTime dateGot;
                    string[] date = values[3].Split('-');
                    if (date.Length - 1 == 1)
                        dateGot = new DateTime(int.Parse(date[0]), int.Parse(date[1]), 1);
                    else if (date.Length - 1 == 0)
                        dateGot = new DateTime(int.Parse(date[0]), 1, 1);
                    else
                        dateGot = DateTime.ParseExact(values[3], "yyyy-MM-dd", null);

                    // Kiem tra ngay phat hanh < dateCheck
                    if (dateGot < dateCheck)
                    {
                        // Lay budget
                        long budgetGot = (values[5].Length > 1 ? long.Parse(values[5].Remove(0, 1)) : long.Parse(values[5]));
                        if (budgetGot == 0)
                            continue;

                        // Lay gross worldwide, usa
                        long grossWorldwideGot = (values[6].Length > 1 ? long.Parse(values[6].Remove(0, 1)) : long.Parse(values[6]));
                        long grossUSAGot = (values[7].Length > 1 ? long.Parse(values[7].Remove(0, 1)) : long.Parse(values[7]));

                        // Neu ton tai gross thi them vao list
                        if (grossWorldwideGot != 0 || grossUSAGot != 0)
                        {
                            listGotDate.Add(dateGot);
                            listGotBudget.Add(budgetGot);
                            listGotGrossWorldwide.Add(grossWorldwideGot);
                            listGotGrossUSA.Add(grossUSAGot);
                        }
                    }
                }
            }
            #endregion

            // 1.2. Nếu trước ngày datecheck ko có phần tử nào thì trả về RETURN_NO_MOVIE_BEFORE_DATECHECK
            if (listGotDate.Count == 0)
                return RETURN_NO_MOVIE_BEFORE_DATECHECK;

            // 1.3. Lọc lại list và chỉ lấy top 5 phim
            #region
            // Khoi tao array index arrIndex tu 0 -> listGotDate.count - 1;
            arrIndexTop5 = new int[listGotDate.Count];
            for (int _i = 0; _i < arrIndexTop5.Length; _i++)
                arrIndexTop5[_i] = _i;

            if (listGotDate.Count > 5)
            {
                // Sort listGotDate theo index cua arrIndex, chi 5 phan tu dau thi dung lai
                int i, j;
                for (j = 0; j < 5; j++)
                {
                    int iMin = j;
                    for (i = j + 1; i < arrIndexTop5.Length; i++)
                    {
                        if (listGotDate[arrIndexTop5[i]] < listGotDate[arrIndexTop5[iMin]])
                        {
                            iMin = i;
                        }
                    }

                    if (iMin != j)
                    {
                        int temp = arrIndexTop5[j];
                        arrIndexTop5[j] = arrIndexTop5[iMin];
                        arrIndexTop5[iMin] = temp;
                    }
                }
            }
            #endregion

            // 2. Tính film state các phim trong top 5
            int countTopFilm = (listGotDate.Count > 5 ? 5 : listGotDate.Count);
            for (int i = 0; i < countTopFilm; i++)
            {
                long gross = (listGotGrossWorldwide[arrIndexTop5[i]] != 0 ? listGotGrossWorldwide[arrIndexTop5[i]] : listGotGrossUSA[arrIndexTop5[i]]);
                long budget = listGotBudget[arrIndexTop5[i]];
                FilmState fs = FilmClassification(budget, gross);
                listTop5MovieState.Add(fs);
            }

            // 3. Tính kết quả
            int countHitSuperHit = 0;
            foreach (FilmState fs in listTop5MovieState)
            {
                if (fs == FilmState.HIT || fs == FilmState.SUPERHIT)
                    countHitSuperHit++;
            }
            result = (double)countHitSuperHit / (double)listTop5MovieState.Count;

            return result;
        }

        public static double GetWriterValue(string idWriter, DateTime dateCheck)
        {
            /**
            Hàm trả về:
               -1 : nếu trước datecheck ko có phim nào có writer là idWriter
               -2 : nếu ko tìm thấy các file data của task 5
            */
            double result;
            const int RETURN_NO_MOVIE_BEFORE_DATECHECK = -1;
            const int RETURN_FILE_NOT_FOUND = -2;
            const string DIR_DATA_NAME = "Task5";
            List<DateTime> listGotDate = new List<DateTime>();
            List<long> listGotBudget = new List<long>();
            List<long> listGotGrossWorldwide = new List<long>();
            List<long> listGotGrossUSA = new List<long>();
            int[] arrIndexTop5;
            List<FilmState> listTop5MovieState = new List<FilmState>();

            // 1. Lấy top 5 phim gần nhất trước ngày dateCheck

            // 1.1. Lấy các phim trước ngày dateCheck
            #region
            // Kiểm tra có đầy đủ 25 part data của task 5 không
            List<string> filePaths = new List<string>();
            for (int part = 0; part < 25; part++)
            {
                filePaths.Add(System.Web.HttpContext.Current.Server.MapPath("~/FilmPreditionData/Task5/task5.part" + part + ".txt"));
                if (File.Exists(filePaths[part]) == false)
                    return RETURN_FILE_NOT_FOUND;
            }

            // Duyệt từng dòng, kiểm tra các đkiện của phim: date khác none, date < dateCheck, phim có budget && có gross (worldwide || usa)
            foreach (string filePath in filePaths)
            {
                var linesRead = File.ReadLines(filePath);
                foreach (string line in linesRead)
                {
                    string[] values = line.Split('\t');

                    // Kiem tra id dao dien (*)
                    if (values[2] != idWriter) continue;

                    // Kiem tra ngay phat hanh khac @none
                    if (values[3] == MOVIE_RELEASE_DATE_NONE)
                        continue;

                    // Lay ngay phay hanh
                    DateTime dateGot;
                    string[] date = values[3].Split('-');
                    if (date.Length - 1 == 1)
                        dateGot = new DateTime(int.Parse(date[0]), int.Parse(date[1]), 1);
                    else if (date.Length - 1 == 0)
                        dateGot = new DateTime(int.Parse(date[0]), 1, 1);
                    else
                        dateGot = DateTime.ParseExact(values[3], "yyyy-MM-dd", null);

                    // Kiem tra ngay phat hanh < dateCheck
                    if (dateGot < dateCheck)
                    {
                        // Lay budget
                        long budgetGot = (values[5].Length > 1 ? long.Parse(values[5].Remove(0, 1)) : long.Parse(values[5]));
                        if (budgetGot == 0)
                            continue;

                        // Lay gross worldwide, usa
                        long grossWorldwideGot = (values[6].Length > 1 ? long.Parse(values[6].Remove(0, 1)) : long.Parse(values[6]));
                        long grossUSAGot = (values[7].Length > 1 ? long.Parse(values[7].Remove(0, 1)) : long.Parse(values[7]));

                        // Neu ton tai gross thi them vao list
                        if (grossWorldwideGot != 0 || grossUSAGot != 0)
                        {
                            listGotDate.Add(dateGot);
                            listGotBudget.Add(budgetGot);
                            listGotGrossWorldwide.Add(grossWorldwideGot);
                            listGotGrossUSA.Add(grossUSAGot);
                        }
                    }
                }
            }
            #endregion

            // 1.2. Nếu trước ngày datecheck ko có phần tử nào thì trả về RETURN_NO_MOVIE_BEFORE_DATECHECK
            if (listGotDate.Count == 0)
                return RETURN_NO_MOVIE_BEFORE_DATECHECK;

            // 1.3. Lọc lại list và chỉ lấy top 5 phim
            #region
            // Khoi tao array index arrIndex tu 0 -> listGotDate.count - 1;
            arrIndexTop5 = new int[listGotDate.Count];
            for (int _i = 0; _i < arrIndexTop5.Length; _i++)
                arrIndexTop5[_i] = _i;

            if (listGotDate.Count > 5)
            {
                // Sort listGotDate theo index cua arrIndex, chi 5 phan tu dau thi dung lai
                int i, j;
                for (j = 0; j < 5; j++)
                {
                    int iMin = j;
                    for (i = j + 1; i < arrIndexTop5.Length; i++)
                    {
                        if (listGotDate[arrIndexTop5[i]] < listGotDate[arrIndexTop5[iMin]])
                        {
                            iMin = i;
                        }
                    }

                    if (iMin != j)
                    {
                        int temp = arrIndexTop5[j];
                        arrIndexTop5[j] = arrIndexTop5[iMin];
                        arrIndexTop5[iMin] = temp;
                    }
                }
            }
            #endregion

            // 2. Tính film state các phim trong top 5
            int countTopFilm = (listGotDate.Count > 5 ? 5 : listGotDate.Count);
            for (int i = 0; i < countTopFilm; i++)
            {
                long gross = (listGotGrossWorldwide[arrIndexTop5[i]] != 0 ? listGotGrossWorldwide[arrIndexTop5[i]] : listGotGrossUSA[arrIndexTop5[i]]);
                long budget = listGotBudget[arrIndexTop5[i]];
                FilmState fs = FilmClassification(budget, gross);
                listTop5MovieState.Add(fs);
            }

            // 3. Tính kết quả
            int countHitSuperHit = 0;
            foreach (FilmState fs in listTop5MovieState)
            {
                if (fs == FilmState.HIT || fs == FilmState.SUPERHIT)
                    countHitSuperHit++;
            }
            result = (double)countHitSuperHit / (double)listTop5MovieState.Count;

            return result;
        }

        public static double GetMusicBy(string idMusicBy, DateTime dateCheck)
        {
            /**
            Hàm trả về:
               -1 : nếu trước datecheck ko có phim nào có music by là idMusicBy
               -2 : nếu ko tìm thấy các file data của task 6
            */
            double result;
            const int RETURN_NO_MOVIE_BEFORE_DATECHECK = -1;
            const int RETURN_FILE_NOT_FOUND = -2;
            const string DIR_DATA_NAME = "Task6";
            List<DateTime> listGotDate = new List<DateTime>();
            List<long> listGotBudget = new List<long>();
            List<long> listGotGrossWorldwide = new List<long>();
            List<long> listGotGrossUSA = new List<long>();
            int[] arrIndexTop5;
            List<FilmState> listTop5MovieState = new List<FilmState>();

            // 1. Lấy top 5 phim gần nhất trước ngày dateCheck

            // 1.1. Lấy các phim trước ngày dateCheck
            #region
            // Kiểm tra có đầy đủ 25 part data của task 6 không
            List<string> filePaths = new List<string>();
            for (int part = 0; part < 25; part++)
            {
                filePaths.Add(System.Web.HttpContext.Current.Server.MapPath("~/FilmPreditionData/Task6/task6.part" + part + ".txt"));
                if (File.Exists(filePaths[part]) == false)
                    return RETURN_FILE_NOT_FOUND;
            }

            // Duyệt từng dòng, kiểm tra các đkiện của phim: date khác none, date < dateCheck, phim có budget && có gross (worldwide || usa)
            foreach (string filePath in filePaths)
            {
                var linesRead = File.ReadLines(filePath);
                foreach (string line in linesRead)
                {
                    string[] values = line.Split('\t');

                    // Kiem tra id dao dien (*)
                    if (values[2] != idMusicBy) continue;

                    // Kiem tra ngay phat hanh khac @none
                    if (values[3] == MOVIE_RELEASE_DATE_NONE)
                        continue;

                    // Lay ngay phay hanh
                    DateTime dateGot;
                    string[] date = values[3].Split('-');
                    if (date.Length - 1 == 1)
                        dateGot = new DateTime(int.Parse(date[0]), int.Parse(date[1]), 1);
                    else if (date.Length - 1 == 0)
                        dateGot = new DateTime(int.Parse(date[0]), 1, 1);
                    else
                        dateGot = DateTime.ParseExact(values[3], "yyyy-MM-dd", null);

                    // Kiem tra ngay phat hanh < dateCheck
                    if (dateGot < dateCheck)
                    {
                        // Lay budget
                        long budgetGot = (values[5].Length > 1 ? long.Parse(values[5].Remove(0, 1)) : long.Parse(values[5]));
                        if (budgetGot == 0)
                            continue;

                        // Lay gross worldwide, usa
                        long grossWorldwideGot = (values[6].Length > 1 ? long.Parse(values[6].Remove(0, 1)) : long.Parse(values[6]));
                        long grossUSAGot = (values[7].Length > 1 ? long.Parse(values[7].Remove(0, 1)) : long.Parse(values[7]));

                        // Neu ton tai gross thi them vao list
                        if (grossWorldwideGot != 0 || grossUSAGot != 0)
                        {
                            listGotDate.Add(dateGot);
                            listGotBudget.Add(budgetGot);
                            listGotGrossWorldwide.Add(grossWorldwideGot);
                            listGotGrossUSA.Add(grossUSAGot);
                        }
                    }
                }
            }
            #endregion

            // 1.2. Nếu trước ngày datecheck ko có phần tử nào thì trả về RETURN_NO_MOVIE_BEFORE_DATECHECK
            if (listGotDate.Count == 0)
                return RETURN_NO_MOVIE_BEFORE_DATECHECK;

            // 1.3. Lọc lại list và chỉ lấy top 5 phim
            #region
            // Khoi tao array index arrIndex tu 0 -> listGotDate.count - 1;
            arrIndexTop5 = new int[listGotDate.Count];
            for (int _i = 0; _i < arrIndexTop5.Length; _i++)
                arrIndexTop5[_i] = _i;

            if (listGotDate.Count > 5)
            {
                // Sort listGotDate theo index cua arrIndex, chi 5 phan tu dau thi dung lai
                int i, j;
                for (j = 0; j < 5; j++)
                {
                    int iMin = j;
                    for (i = j + 1; i < arrIndexTop5.Length; i++)
                    {
                        if (listGotDate[arrIndexTop5[i]] < listGotDate[arrIndexTop5[iMin]])
                        {
                            iMin = i;
                        }
                    }

                    if (iMin != j)
                    {
                        int temp = arrIndexTop5[j];
                        arrIndexTop5[j] = arrIndexTop5[iMin];
                        arrIndexTop5[iMin] = temp;
                    }
                }
            }
            #endregion

            // 2. Tính film state các phim trong top 5
            int countTopFilm = (listGotDate.Count > 5 ? 5 : listGotDate.Count);
            for (int i = 0; i < countTopFilm; i++)
            {
                long gross = (listGotGrossWorldwide[arrIndexTop5[i]] != 0 ? listGotGrossWorldwide[arrIndexTop5[i]] : listGotGrossUSA[arrIndexTop5[i]]);
                long budget = listGotBudget[arrIndexTop5[i]];
                FilmState fs = FilmClassification(budget, gross);
                listTop5MovieState.Add(fs);
            }

            // 3. Tính kết quả
            int countHitSuperHit = 0;
            foreach (FilmState fs in listTop5MovieState)
            {
                if (fs == FilmState.HIT || fs == FilmState.SUPERHIT)
                    countHitSuperHit++;
            }
            result = (double)countHitSuperHit / (double)listTop5MovieState.Count;

            return result;
        }

        public static long GetBudgetFilm(string idFilm)
        {
            return 0;
        }

        public static FilmState FilmClassification(long budget, long domesticWw)
        {
            long result = domesticWw - budget;
            //flop movie 
            if (result < 100000000) return FilmState.FLOP;
            //hit movie 
            if (result < 200000000) return FilmState.HIT;
            //super hit 
            return FilmState.SUPERHIT;
        }

        public static double NormalizeReleaseDate(DateTime releaseDate, TypeData typeData)
        {
            if (typeData == TypeData.TRAIN)
            {
                //usa holiday session
                if ((int)releaseDate.DayOfWeek == 0 || (int)releaseDate.DayOfWeek == 5 ||
                    (int)releaseDate.DayOfWeek == 6) return 0.9;
                return 0.7;
            }
            else
            if (typeData == TypeData.PREDICTION)
            {
                //Vn holiday session
                if ((int)releaseDate.DayOfWeek == 0 || (int)releaseDate.DayOfWeek == 5 ||
                    (int)releaseDate.DayOfWeek == 6) return 0.9;
                return 0.7;
            }
            return 0.5;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filmName"></param>
        /// <param name="typeData">Type = 0: Training Data, Type = 1: Predition value</param>
        /// <returns></returns>
        public static TrainingData CreateTrainData(string filmName, TypeData typeData)
        {
            TrainingData result = new TrainingData();

            //processing data 
            IMDBModel film = GetMovieInformation(filmName);
            if (film == null) return null;

            //first actor value 
            result.FirstActorValue = film.MovieStarActorText[0] == null
                ? 0
                : GetActorValue(film.MovieStarActorText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //second actor value
            result.SecondActorValue = film.MovieStarActorText[1] == null
                ? 0
                : GetActorValue(film.MovieStarActorText[1], Convert.ToDateTime(film.MovieReleaseDate));

            //director value
            result.DirectorValue = film.MovieDirectorText[0] == null
                ? 0
                : GetDirectorValue(film.MovieDirectorText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //production company
            result.ProductionCompanyValue = film.MovieProductionText[0] == null
                ? 0
                : GetProductionCompanyValue(film.MovieProductionText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //writer value
            result.GetWriterValue = film.MovieWriterText[0] == null
                ? 0
                : GetWriterValue(film.MovieWriterText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //music value 
            result.GetMusicBy = film.MovieMusicbyText[0] == null
                ? 0
                : GetMusicBy(film.MovieMusicbyText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //budget value - Chưa xử lý budget
            result.BugetValue = film.MovieBudget;

            //release date value
            result.ReleaseDateValue = NormalizeReleaseDate(Convert.ToDateTime(film.MovieReleaseDate), typeData);

            //output value
            if (typeData == TypeData.TRAIN)
            {
                //train data 
                long domestic = 0;
                if (film.MovieGrossWorldwide != 0) domestic = film.MovieGrossWorldwide;
                else
                    domestic = film.MovieGrossUSA;
                var resultClasscification = FilmClassification(film.MovieBudget, domestic);

                //film classcification 
                if (resultClasscification == FilmState.FLOP)
                {
                    result.FirstResultPart = 1;
                    result.SecondResultPart = 0;
                    result.ThirdResultPart = 0;
                }
                else if (resultClasscification == FilmState.HIT)
                {
                    result.FirstResultPart = 0;
                    result.SecondResultPart = 1;
                    result.ThirdResultPart = 0;
                }
                else if (resultClasscification == FilmState.SUPERHIT)
                {
                    result.FirstResultPart = 0;
                    result.SecondResultPart = 0;
                    result.ThirdResultPart = 1;
                }
            }
            else if (typeData == TypeData.PREDICTION)
            {
                //Predition
                result.FirstResultPart = result.SecondResultPart = result.ThirdResultPart = 0;
            }

            return result;
        }

        public static void PrintTrainData(TrainingData temp)
        {
            Console.WriteLine("First actor " + temp.FirstActorValue);
            Console.WriteLine("Second actor " + temp.SecondActorValue);
            Console.WriteLine("Director " + temp.DirectorValue);
            Console.WriteLine("Production Company " + temp.ProductionCompanyValue);
            Console.WriteLine("Writer " + temp.GetWriterValue);
            Console.WriteLine("Music by " + temp.GetMusicBy);
            Console.WriteLine("Budget  " + temp.BugetValue);
            Console.WriteLine("Release date " + temp.ReleaseDateValue);
            Console.WriteLine("First value " + temp.FirstResultPart);
            Console.WriteLine("Secons value " + temp.SecondResultPart);
            Console.WriteLine("Third value " + temp.ThirdResultPart);
        }
        public static void CreateTrainDataFile(string pathIn, string pathOut)
        {
            using (var reader = new StreamReader(pathIn))
            {
                using (var writer = new StreamWriter(pathOut))
                {
                    string film = string.Empty;
                    film = reader.ReadLine();
                    while (film != null)
                    {
                        try
                        {
                            TrainingData result = CreateTrainData(film, TypeData.TRAIN);
                            if (result != null)
                            {
                                writer.WriteLine(result.FirstActorValue + "\t" + result.SecondActorValue + "\t" +
                                                 result.DirectorValue
                                                 + "\t" + result.ProductionCompanyValue + "\t" + result.GetWriterValue +
                                                 "\t" + result.GetMusicBy
                                                 + "\t" + result.BugetValue + "\t" + result.ReleaseDateValue + "\t" +
                                                 result.FirstResultPart
                                                 + "\t" + result.SecondResultPart + "\t" + result.ThirdResultPart
                                                 + "\t" + film);
                            }
                        }
                        catch (Exception ex)
                        {
                            writer.WriteLine(film);
                        }
                        film = reader.ReadLine();
                    }
                }
            }
        }

        public static TrainingData TrainingFilmAndPredition(IMDBModel film)
        {
            TrainingData result = new TrainingData();
            if (film == null) return null;

            //first actor value 
            result.FirstActorValue = film.MovieStarActorText[0] == null
                ? 0
                : GetActorValue(film.MovieStarActorText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //second actor value
            result.SecondActorValue = film.MovieStarActorText[1] == null
                ? 0
                : GetActorValue(film.MovieStarActorText[1], Convert.ToDateTime(film.MovieReleaseDate));

            //director value
            result.DirectorValue = film.MovieDirectorText[0] == null
                ? 0
                : GetDirectorValue(film.MovieDirectorText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //production company
            result.ProductionCompanyValue = film.MovieProductionText[0] == null
                ? 0
                : GetProductionCompanyValue(film.MovieProductionText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //writer value
            result.GetWriterValue = film.MovieWriterText[0] == null
                ? 0
                : GetWriterValue(film.MovieWriterText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //music value 
            result.GetMusicBy = film.MovieMusicbyText[0] == null
                ? 0
                : GetMusicBy(film.MovieMusicbyText[0], Convert.ToDateTime(film.MovieReleaseDate));

            //budget value - Chưa xử lý budget
            result.BugetValue = film.MovieBudget;

            //release date value

            result.ReleaseDateValue = NormalizeReleaseDate(Convert.ToDateTime(film.MovieReleaseDate), TypeData.TRAIN);

            //output value
            //Predition
            result.FirstResultPart = result.SecondResultPart = result.ThirdResultPart = 0;
            return result;
        }

        private static double[][] LoadData()
        {
            double[][] allData = new double[849][];
            int columns = 0, rows = 0;
            using (var reader = new StreamReader("datasetTrain.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    int temp = 0;
                    allData[rows] = new double[11];
                    string[] content = line.Split(new char[] { '\t' });
                    columns = 0;
                    foreach (string t in content)
                    {
                        if (t.Trim() != "")
                        {
                            double iValue = Double.Parse(content[temp].ToString());

                            allData[rows][columns] = iValue;
                            temp++;
                        }
                        columns++;
                    }
                    rows++;
                    line = reader.ReadLine();
                }
            }
            return allData;
        }
        private static void CreateFileWeight(double[] weights)
        {
            using (var writer = new StreamWriter("dataWeight.txt"))
            {
                foreach (var item in weights)
                {
                    writer.WriteLine(item);
                }
            }
        }

        public static int PreditionType(int type)
        {
            Random r = new Random();
            int temp = r.Next(0, 2);
            if (temp == 1) return r.Next(0, 3);
            return type;
        }

        private static double[] ReadWeights()
        {
            double[] weights = new double[207];
            using (var reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/FilmPreditionData/dataWeight.txt")))
            {
                int count = 0;
                string temp = reader.ReadLine();
                while (temp != null)
                {
                    weights[count] = Convert.ToDouble(temp);
                    count++;
                    temp = reader.ReadLine();
                }
            }
            return weights;
        }

        private static double[][] LoadDataTrain()
        {
            double[][] allData = new double[849][];
            int columns = 0, rows = 0;
            using (var reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/FilmPreditionData/datasetTrain.txt")))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    int temp = 0;
                    allData[rows] = new double[11];
                    string[] content = line.Split(new char[] { '\t' });
                    columns = 0;
                    foreach (string t in content)
                    {
                        if (t.Trim() != "")
                        {
                            double iValue = Double.Parse(content[temp].ToString());

                            allData[rows][columns] = iValue;
                            temp++;
                        }
                        columns++;
                    }
                    rows++;
                    line = reader.ReadLine();
                }
            }
            return allData;
        }

        public static FilmState StartTrain(IMDBModel model)
        {
            TrainingData predition = TrainingFilmAndPredition(model);
            if (predition.FirstActorValue < 0) predition.FirstActorValue = 0;
            if (predition.SecondActorValue < 0) predition.SecondActorValue = 0;
            if (predition.DirectorValue < 0) predition.DirectorValue = 0;
            if (predition.GetWriterValue < 0) predition.GetWriterValue = 0;
            if (predition.GetMusicBy < 0) predition.GetMusicBy = 0;
            if (predition.ProductionCompanyValue < 0) predition.ProductionCompanyValue = 0;

            double[] xValue = new double[8];
            xValue[0] = predition.FirstActorValue;
            xValue[1] = predition.SecondActorValue;
            xValue[2] = predition.DirectorValue;
            xValue[3] = predition.ProductionCompanyValue;
            xValue[4] = predition.GetWriterValue;
            xValue[5] = predition.GetMusicBy;
            xValue[6] = predition.BugetValue;
            xValue[7] = predition.ReleaseDateValue;

            double[][] allData = LoadDataTrain();
            double[][] trainData = null;
            double[][] testData = null;
            MakeTrainTest(allData, 800, out trainData, out testData);

            int numInput = 8;
            int numHidden = 17;
            int numOutput = 3;
            NeuralNetwork nn = new NeuralNetwork(numInput, numHidden, numOutput);
            int maxEpochs = 1000;
            double learnRate = 0.05;
            double momentum = 0.01;
            nn.Train(trainData, maxEpochs, learnRate, momentum);

            var valueY = nn.ComputeOutputs(xValue);
            int type = nn.MaxIndex(valueY);
            type = PreditionType(type);
            if (type == 0) return FilmState.FLOP;
            if (type == 1) return FilmState.HIT;
            if (type == 2 ) return FilmState.SUPERHIT;
            return FilmState.DEFAULT;
        }

        public static void MakeTrainTest(double[][] allData, int seed, out double[][] trainData, out double[][] testData)
        {
            //Split allData into 80 % trainData and 20 % testData.
            Random rnd = new Random(seed);
            int totRows = allData.Length;
            int numCols = allData[0].Length;

            int trainRows = (int)(totRows * 0.80); // Hard-coded 80-20 split.
            int testRows = totRows - trainRows;

            trainData = new double[trainRows][];
            testData = new double[testRows][];

            double[][] copy = new double[allData.Length][]; // Make a reference copy.

            for (int i = 0; i < copy.Length; ++i)
                copy[i] = allData[i];

            //Scramble row order of copy.
            for (int i = 0; i < copy.Length; ++i)
            {
                int r = rnd.Next(i, copy.Length);
                double[] tmp = copy[r];
                copy[r] = copy[i];
                copy[i] = tmp;
            }

            //Copy first trainRows from copy[][] to trainData[][].
            for (int i = 0; i < trainRows; ++i)
            {
                trainData[i] = new double[numCols];
                for (int j = 0; j < numCols; ++j)
                {
                    trainData[i][j] = copy[i][j];
                }
            }

            //Copy testRows rows of allData[] into testData[][].
            for (int i = 0; i < testRows; ++i) // i points into testData[][] .
            {
                testData[i] = new double[numCols];
                for (int j = 0; j < numCols; ++j)
                {
                    testData[i][j] = copy[i + trainRows][j];
                }
            }

        }
    }
}
