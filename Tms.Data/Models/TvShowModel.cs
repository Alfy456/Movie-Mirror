using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms.Domain.Models
{
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