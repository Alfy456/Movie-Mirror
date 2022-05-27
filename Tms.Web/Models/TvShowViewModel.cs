using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms.Web.Models
{
    public class TvShowViewModel
    {
        public int Id { get; set; }
        public int TvShowId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Premiered { get; set; }
        public Nullable<System.DateTime> Ended { get; set; }
        public Nullable<System.DateTime> UpdatedWhen { get; set; }
        public string Image { get; set; }
    }
}