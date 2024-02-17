using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Tms.Enum;

namespace Tms.Web.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public Nullable<int> MovieId { get; set; }
        public string Name { get; set; }

        [Description("Select Language")]
        public Language Language { get; set; }

        [Description("Select OTT Platform")]
        public Ott OttPlatform { get; set; }

        [Display(Name = "Upload To Telegram")]
        public bool IsUploadedToTelegram { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<System.DateTime> TrailerDate { get; set; }
        public Nullable<System.DateTime> TeaserDate { get; set; }
        public Nullable<System.DateTime> OttReleaseDate { get; set; }
        public string Image { get; set; }
    }
}