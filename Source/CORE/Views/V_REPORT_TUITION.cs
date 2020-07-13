using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_REPORT_TUITION : BusinessObject
    {
        public int ScheduleId { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public decimal TuitionStudies { get; set; }
    }
}
