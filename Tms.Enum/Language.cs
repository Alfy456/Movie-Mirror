using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Enum
{
    public enum Language
    {
        Malayalam=0,
        Tamil=1,
        Hindhi=2,
        English=3,
        Telugu = 4,
        Korean = 5,
        Unknown =6,
        TvShow = 7,

    }
    public enum Ott
    {
        Theatre= 0,
        Netflix = 1,
        Amazon= 2,
        Sony=3,
        Disney = 4,
        Apple=5,
        Manorama=6,
        Sun=7,
        Zee=8,
        Unknown = 9,
       
    }
    public enum Status
    {
        Coming = 0,
        Running = 1,
        Ended = 2,
        Unknown = 99,
    }
}
