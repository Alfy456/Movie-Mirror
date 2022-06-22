using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Data;
using Tms.Enum;

namespace Tms.Domain
{
    public class MovieBL
    {
        private TmsEntities db = new TmsEntities();
        public List<Movie> GetAllMovies()
        {
            List <Movie> lstAllMovies = db.Movies.ToList();
            return lstAllMovies;
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
    }
}
