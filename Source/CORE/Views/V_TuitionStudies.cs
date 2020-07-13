using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_TuitionStudies : BusinessObject
    {
        public int ScheduleId { get; set; }
        public DateTime TrackingDate { get; set; }
        public int CountNumber { get; set; }
        public decimal TuitionStudies { get; set; }

    }
}
