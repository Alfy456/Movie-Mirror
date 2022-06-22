using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms.Web.Models
{
    public class AnniversaryViewModel
    {
        public string Name { get; set; }

        public DateTime? Premiered { get; set; }

        public int Age { get; set; }

        public string ImageLink { get; set; }
    }
}