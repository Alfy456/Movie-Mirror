using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tms.Web.Models
{
    public class ActorViewModel
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
    }
}