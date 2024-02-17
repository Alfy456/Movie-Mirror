using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Data.Models
{
    public  class JobModel
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime CreatedWhen { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
}
