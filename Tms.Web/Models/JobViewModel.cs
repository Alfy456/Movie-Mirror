using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tms.Web.Models
{
    public class JobViewModel
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedWhen { get; set; }
        public DateTime? UpdatedWhen { get; set; }
    }
}