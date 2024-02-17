using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tms.Enum;

namespace Tms.Web.Models
{
    public class TvShowViewModel
    {
        public int Id { get; set; }
        public int TvShowId { get; set; }
        public string Name { get; set; }
        public Language Language { get; set; }
        public Status Status { get; set; }
        public Nullable<System.DateTime> Premiered { get; set; }
        public Nullable<System.DateTime> Ended { get; set; }
        public Nullable<System.DateTime> UpdatedWhen { get; set; }
        public string Image { get; set; }
        public Ott OttPlatform { get; set; }
        public DateTime? NewSeasonReleaseDate { get; set; }
        public DateTime? TrailerDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsManualAddition { get; set; }
    }
    public class ShowMainInformation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public string status { get; set; }
        public DateTime? premiered { get; set; }
        public DateTime? ended { get; set; }
        public Image image { get; set; }

    }
    public class PersonMainInformation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string gender { get; set; }
        public DateTime? birthday { get; set; }
        public Image ImageInformation { get; set; }
    }
    public class Image
    {
        public string medium { get; set; }
        public string original { get; set; }
    }
}