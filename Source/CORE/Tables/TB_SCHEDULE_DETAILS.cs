using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_SCHEDULE_DETAILS : BusinessObject
    {
        [PrimaryKey]
        public int ScheduleDetailId { get; set; }
        public DateTime ScheduleDetailDate { get; set; }
        public TimeSpan ScheduleDetailTimeFrom { get; set; }
        public TimeSpan ScheduleDetailTimeTo { get; set; }
        public string ScheduleDetailNote { get; set; }
        public int ScheduleDetailScheduleId { get; set; }
        public TB_SCHEDULE_DETAILS() { }

    }
}
