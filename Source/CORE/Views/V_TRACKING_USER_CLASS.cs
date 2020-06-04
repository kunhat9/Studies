using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_TRACKING_USER_CLASS
    {
        public int userId { get; set; }
        public int schedueId { get; set; }
        public List<DateTime> dateTracking { get; set; }
    }
}
