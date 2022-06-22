using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tms.Data;
using Tms.Domain.Models;
using Tms.Enum;

namespace Tms.Domain
{
    public class TvShowBL
    {
        private TmsEntities db = new TmsEntities();
        public List<TvShow> GetAllTvShows()
        {
            List <TvShow> lstAllTvShows = db.TvShows.ToList();
            return lstAllTvShows;
        }

        public TvShow GetTvShowModelById(int? p_intId)
        {
            TvShow objTvShow = db.TvShows.Find(p_intId);
            return objTvShow;
        }

        public void AddTvShow(TvShow p_objTvShow)
        {
            db.TvShows.Add(p_objTvShow);
            db.SaveChanges();
        }

        public void EditTvShow(TvShow p_objTvShow)
        {
            db.Entry(p_objTvShow).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveTvShow(TvShow p_objTvShow)
        {
            db.TvShows.Remove(p_objTvShow);
            db.SaveChanges();
        }

        public void UpdateTvShowsBulk()
        { 
            var webURL = "https://api.tvmaze.com/updates/shows";
            WebClient wC = new WebClient();

            // Get a string representation of our JSON
            string jsonText = wC.DownloadString(webURL);

            var temp = JsonConvert.DeserializeObject<Dictionary<int, string>>(jsonText);

            DateTime dtNow = DateTime.Now;

            var xml = new XDocument(
                new XElement("TvShows",
                    from tmp in temp
                    select new XElement("TvShow",
                        new XAttribute("TvShowId", tmp.Key),
                        new XAttribute("UpdatedWhen", dtNow))));

            var p0 = new SqlXml(xml.CreateReader());
            var sql = ResourceHelpers.GetResourceFile("Tms.Domain.InsertTvSeries.sql");

            db.Database.CommandTimeout = 0;
            db.Database.ExecuteSqlCommand(sql, new SqlParameter("p0", p0));
        }

        public void UpdateTvShowsInformationBulk()
        {
            bool blnExit = true;
            int intId = 0;
            do
            {
                try

                {
                    var webURL = "https://api.tvmaze.com/shows?page=" + intId;

                    WebClient wC = new WebClient();
                    string jsonText = wC.DownloadString(webURL);
                    var ObjShowMainInformation = JsonConvert.DeserializeObject<List<ShowMainInformation>>(jsonText);

                    DateTime dtNow = DateTime.Today;
                    DateTime dtDefaultDate = new DateTime(1753, 01, 01);

                    string strImageLink = "Default";
                    var xml = new XDocument(
                new XElement("TvShows",
                    from tmp in ObjShowMainInformation
                    select new XElement("TvShow",
                        new XAttribute("TvShowId", tmp.id),

                        new XAttribute("Name", tmp.name),
                               new XAttribute("Language", tmp.language = tmp.language == null ? "Default" : tmp.language),

                            new XAttribute("Status", tmp.status),

                                  new XAttribute("Premiered", tmp.premiered = tmp.premiered == null ? dtDefaultDate : tmp.premiered),

                                     new XAttribute("Ended", tmp.ended = tmp.ended == null ? dtDefaultDate : tmp.ended),

                                      new XAttribute("Image", strImageLink = tmp.image == null ? "Default" : tmp.image.medium = tmp.image.medium == null ? "Default" : tmp.image.medium),

                        new XAttribute("UpdatedWhen", dtNow))));


                    var p0 = new SqlXml(xml.CreateReader());


                    var sql = ResourceHelpers.GetResourceFile("Tms.Domain.InsertTvSeries.sql");

                    db.Database.CommandTimeout = 0;
                    db.Database.ExecuteSqlCommand(sql, new SqlParameter("p0", p0));
                    intId++;
                }
                catch (Exception e)
                {
                    blnExit = false;

                }
            }
            while (blnExit);
        }

        public void CreateMovie(Movie p_objMovie)
        {
            db.Movies.Add(p_objMovie);
            db.SaveChanges();
        }
        public Language GetEnumFromMovieName(String p_strMovieName)
        {
            return Language.Malayalam;
            //switch (p_strMovieName)
            //{
            //    case 1: return Language.Malayalam;
            //        break;
            //    case 2: 
            //}
        }

        public IEnumerable<TvShow> GetAnniversary(DateTime p_dtTomorrow)
        {
            var tvshows = (from t in db.TvShows
                           where
                           t.Premiered.Value.Day == p_dtTomorrow.Day &&
                           t.Premiered.Value.Month == p_dtTomorrow.Month
                           select t).ToList().OrderByDescending(t => t.Premiered);
            return tvshows;
        }
    }
}
