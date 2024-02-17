using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms.Web.Models
{
    public class ReleaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Platform { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}