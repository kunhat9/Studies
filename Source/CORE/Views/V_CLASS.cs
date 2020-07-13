using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_CLASS : BusinessObject
    {
        [PrimaryKey]
        public int ScheduleId { get; set; }
        public string ScheduleCode { get; set; }
        public DateTime ScheduleDateCreate { get; set; }
        public string ScheduleStatus { get; set; }
        public string ScheduleDateBegin { get; set; }
        public string ScheduleDateEnd { get; set; }
        public decimal SchedulePrice { get; set; }
        public int ScheduleUserId { get; set; }
        public int ScheduleIdBoxSubjectId { get; set; }
        public string ScheduleFileId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public int ScheduleDetailId { get; set; }
        public string ScheduleDetailDayOfWeek { get; set; }
        public TimeSpan ScheduleDetailTimeFrom { get; set; }
        public TimeSpan ScheduleDetailTimeTo { get; set; }
        public string ScheduleDetailNote { get; set; }

        public int ScheduleDetailRoomClass { get; set; }
    }
}
