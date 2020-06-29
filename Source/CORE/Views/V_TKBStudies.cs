using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_TKBStudies : BusinessObject
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }
        public string UserStatus { get; set; }
        public string ScheduleDetailDayOfWeek { get; set; }
        public TimeSpan ScheduleDetailTimeFrom { get; set; }
        public TimeSpan ScheduleDetailTimeTo { get; set; }
    }
}
