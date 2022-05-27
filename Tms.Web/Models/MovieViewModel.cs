using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms.Web.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public Nullable<int> MovieId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string OttPlatform { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<System.DateTime> TrailerDate { get; set; }
        public Nullable<System.DateTime> TeaserDate { get; set; }
        public Nullable<System.DateTime> OttReleaseDate { get; set; }
        public string Image { get; set; }
    }
}