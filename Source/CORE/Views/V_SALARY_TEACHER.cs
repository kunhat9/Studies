using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_SALARY_TEACHER : BusinessObject
    {
        public int ScheduleId { get; set; }
        public DateTime TrackingDate { get; set; }
        public int CountStudies { get; set; }
        public decimal SalaryTeacher { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
    }
   
}
