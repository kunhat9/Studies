using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_TEACHING_SCHEDULES : BusinessObject
    {
        [PrimaryKey]
        public int TeachingScheduleId { get; set; }
        public string TeachingScheduleDayOfWeek { get; set; }
        public TimeSpan TeachingScheduleTimeFrom { get; set; }
        public TimeSpan TeachingScheduleTimeTo { get; set; }
        public string TeachingScheduleNote { get; set; }
        public int TeachingScheduleUserId { get; set; }
        public TB_TEACHING_SCHEDULES() { }

    }
}
