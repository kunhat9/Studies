using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class HomePage : BusinessObject
    {
        public int CountNewClass { get; set; }
        public int CountNewStudies { get; set; }
        public int CountNewTeacher { get; set; }
        public decimal TotalRevenue { get; set; }
        public HomePage() { }

    }
}
