using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_SCHEDULE_DETAILS : BusinessObject
    {
        public int ScheduleId { get; set; }
        public string ScheduleCode { get; set; }
        public DateTime ScheduleDateCreate { get; set; }
        public string ScheduleStatus { get; set; }
        public string ScheduleDateBegin { get; set; }
        public string ScheduleDateEnd { get; set; }
        public decimal SchedulePrice { get; set; }
        public int ScheduleUserId { get; set; }
        public int ScheduleIdBoxSubjectId { get; set; }
        public int TeachingScheduleId { get; set; }
        public string TeachingScheduleDayOfWeek { get; set; }
        public TimeSpan TeachingScheduleTimeFrom { get; set; }
        public TimeSpan TeachingScheduleTimeTo { get; set; }
        public string TeachingScheduleNote { get; set; }
        public int TeachingScheduleUserId { get; set; }
        public V_SCHEDULE_DETAILS() { }
    }
}
