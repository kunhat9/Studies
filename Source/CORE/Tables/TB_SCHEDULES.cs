using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_SCHEDULES : BusinessObject
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
        public TB_SCHEDULES() { }

    }
}
