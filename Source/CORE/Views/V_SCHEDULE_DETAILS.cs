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
        public int ScheduleDetailId { get; set; }
        public string ScheduleDetailDayOfWeek { get; set; }
        public TimeSpan ScheduleDetailTimeFrom { get; set; }
        public TimeSpan ScheduleDetailTimeTo { get; set; }
        public string ScheduleDetailNote { get; set; }
        public int ScheduleDetailScheduleId { get; set; }
       
        public V_SCHEDULE_DETAILS() { }
    }
}
