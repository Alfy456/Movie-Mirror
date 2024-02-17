using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Data;
using Tms.Enum;

namespace Tms.Domain
{
    public class CommonBL
    {
        private TmsEntities db = new TmsEntities();

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

        public Status GetEnumStatusFromString(String p_strStatus)
        {

            switch (p_strStatus)
            {
                       case "Coming":
                    return Status.Coming;

                case "Running":
                    return Status.Running;

                case "Ended":
                    return Status.Ended;

                default:
                    return Status.Unknown;

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

        public void test()
        { 
        
        }
    }
}
