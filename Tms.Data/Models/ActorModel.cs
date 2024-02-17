using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms.Domain.ActorModels
{
    public class ActorModel
    {
        public int id { get; set; }
        public string name { get; set; }
        //public Country country { get; set; }
        public string gender { get; set; }
        public DateTime? birthday { get; set; }
        public Image image { get; set; }
    }
    public class Image
    {
        public string medium { get; set; }
        public string original { get; set; }
    }
    public class Country
    {
        public string name { get; set; }
        public string code { get; set; }
        public string timezone { get; set; }

    }
}