using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Tables;

namespace CORE.Views
{
    public class V_CLASS_DETAILS
    {
        public int ScheduleId { get; set; }
        public List<V_CLASS> Class { get; set; }
        //public TB_SCHEDULES Schedule { get; set; }
        //public List<TB_SCHEDULE_DETAILS> Schedule_Details { get; set; }
    }
}
