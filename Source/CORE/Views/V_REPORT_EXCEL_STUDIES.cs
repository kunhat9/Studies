using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_REPORT_EXCEL_STUDIES : BusinessObject
    {
        public int ScheduleId { get; set; }
        public string ScheduleCode { get; set; }
        public decimal TuitionStudies { get; set; }
        public int UserId { get; set; }
        public int CountNumber { get; set; }
        public string UserFullName { get; set; }
    }

}
