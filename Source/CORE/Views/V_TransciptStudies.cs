using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_TransciptStudies : BusinessObject
    {
      
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public int ClassScheduleId { get; set; }
        public int PointNumber { get; set; }
        public string PointType { get; set; }
    }
}
