using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tms.Web.Models
{
    public class BirthdayViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? PersonBirthday { get; set; }
        public int Age { get; set; }
        public string ImageLink { get; set; }
    }
}