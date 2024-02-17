using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tms.Data;
using Tms.Domain.Models;
using Tms.Enum;

namespace Tms.Domain
{
    public class MovieBL
    {
        private TmsEntities db = new TmsEntities();
        public List<Movie> GetAllMovies()
        {
            List<Movie> lstAllMovies = db.Movies.ToList();
            return lstAllMovies;
        }

        public void CreateMovie(Movie p_objMovie)
        {
            db.Movies.Add(p_objMovie);
            db.SaveChanges();
        }

        public Language GetEnumLanguageFromString(String p_strMovieLanguage)
        {

            switch (p_strMovieLanguage)
            {
                case "English":
                    return Language.English;

                case "Malayalam":
                    return Language.Malayalam;

                case "Tamil":
                    return Language.Tamil;

                case "Hindhi":
                    return Language.Hindhi;

                case "Korean":
                    return Language.Korean;

                case "Telugu":
                    return Language.Telugu;

                default:
                    return Language.Unknown;

            }
        }

        public Ott GetEnumOttFromString(String p_strMovieOttPlatform)
        {

            switch (p_strMovieOttPlatform)
            {
                case "Theatre":
                    return Ott.Theatre;

                case "Netflix":
                    return Ott.Netflix;

                case "Amazon":
                    return Ott.Amazon;

                case "Sony":
                    return Ott.Sony;

                case "Disney":
                    return Ott.Disney;

                case "Apple":
                    return Ott.Apple;

                case " Manorama":
                    return Ott.Manorama;

                case "Sun":
                    return Ott.Sun;

                case "Zee":
                    return Ott.Zee;

                default:
                    return Ott.Unknown;
            }
        }
        public List<Movie> GetReleaseData(DateTime p_dtDate)
        {

            p_dtDate = p_dtDate.Date;

            List<Movie> lstMovies1 = db.Movies.ToList();

            List<Movie> lstMovies = db.Movies.Where(m =>
                                                    DbFunctions.TruncateTime(m.ReleaseDate) == p_dtDate ||
                                                    DbFunctions.TruncateTime(m.OttReleaseDate) == p_dtDate ||
                                                    DbFunctions.TruncateTime(m.TeaserDate) == p_dtDate ||
                                                    DbFunctions.TruncateTime(m.TrailerDate) == p_dtDate
                                                    ).ToList();
            return lstMovies;

        }
        public DateTime? GetDate(Movie p_objMovie)
        {
            if (p_objMovie.OttReleaseDate != null)
            {
                return p_objMovie.OttReleaseDate;
            }
            else if (p_objMovie.TrailerDate != null)
            {
                return p_objMovie.TrailerDate;
            }
            else if (p_objMovie.TeaserDate != null)
            {
                return p_objMovie.TeaserDate;
            }
            else
            {
                return p_objMovie.ReleaseDate;
            }
        }

        public void EditeMovie(Movie p_objNewMovie)
        {
            db.Entry(p_objNewMovie).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Movie GetMovieById(int id)
        {
            return db.Movies.Where(m => m.Id == id).FirstOrDefault();
        }
        public void MovieBulkUpload()
        {
            var Url = "https://api.themoviedb.org/3/discover/movie";
            var ApiKey = "c8656808145423583f9da1afd7d1e967";
            var Language = "en-US";
            var OriginalLanguage = "ml";
            var SortBy = "release_date.desc";
            int intPageId = 1;
            bool blnExit = true;
            DateTime dtNow = DateTime.Today;
            DateTime dtDefaultDate = new DateTime(1900, 01, 01);
            string strImageLink = "Default";

            do
            {
                try
                {
                    var webURL = Url + "?" + "api_key" + "=" + ApiKey + "&" + "language" + "=" + Language + "&" + "with_original_language" + "=" + OriginalLanguage + "&" + "sort_by" + "=" + SortBy + "&" + "page" + "=" + intPageId;
                    WebClient wC = new WebClient();
                    string jsonText = wC.DownloadString(webURL);
                    MovieInformation ObjMovieInformation = JsonConvert.DeserializeObject<MovieInformation>(jsonText);

                    //db insert

                    //Movie m = new Movie();

                    //foreach (var ObjMovie in ObjMovieInformation.Results)
                    //{
                    //    m.Id = ObjMovie.id;
                    //    m.Name = ObjMovie.title;
                    //    m.Image = ObjMovie.poster_path;
                    //    m.ReleaseDate = ObjMovie.release_date;
                    //    db.Movies.Add(m);
                    //}

                    //db.SaveChanges();

                    var xml = new XDocument(
                                            new XElement
                                                ("Movies",
                                                from tmp in ObjMovieInformation.Results
                                                select new XElement
                                                    ("Movie",
                                                        new XAttribute("MovieId", tmp.id),
                                                        new XAttribute("Name", tmp.title),
                                                        new XAttribute("Language", tmp.original_language = tmp.original_language == null ? "Default" : tmp.original_language),
                                                        new XAttribute("ReleaseDate", tmp.release_date = tmp.release_date == null || tmp.release_date < dtDefaultDate ? dtDefaultDate : tmp.release_date),
                                                        new XAttribute("Image", strImageLink = tmp.poster_path == null ? "Default" : tmp.poster_path),
                                                        new XAttribute("CreatedDate", dtNow),
                                                        new XAttribute("UpdatedDate", dtNow)
                                                    )
                                                )
                                            );

                    //sql embeded

                    var p0 = new SqlXml(xml.CreateReader());
                    var sql = ResourceHelpers.GetResourceFile("Tms.Domain.InsertMovies.sql");
                    db.Database.CommandTimeout = 0;
                   var temp= db.Database.ExecuteSqlCommand(sql, new SqlParameter("p0", p0), new SqlParameter("page", ObjMovieInformation.page),new SqlParameter("language", OriginalLanguage));

                    //stored procedure

                    //var data = db.Database.SqlQuery<Movie>("exec sp_InsertXmlData @xml,@page", new SqlParameter("xml", xml), new SqlParameter("page", ObjMovieInformation.page));

                    if (intPageId >= ObjMovieInformation.total_pages)
                    {
                        blnExit = false;
                    }
                    
                    intPageId++;
                    
                }
                catch (Exception e)
                {
                    blnExit = false;
                }
            }
            while (blnExit);
        }
    }
}
