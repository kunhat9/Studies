using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_TRACKINGS : BusinessObject
    {
        [PrimaryKey]
        public int TrackingId { get; set; }
        public DateTime TrackingDate { get; set; }
        public string TrackingNote { get; set; }
        public int TrackingUserId { get; set; }
        public int TrackingScheduleId { get; set; }
        public string TrackingCheckSalary { get; set; }
        public string TrackingCheckTuition { get; set; }
        public TB_TRACKINGS() { }

    }
}
