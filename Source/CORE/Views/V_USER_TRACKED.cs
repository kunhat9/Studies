using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_USER_TRACKED : BusinessObject
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public DateTime UserDateCreated { get; set; }
        public DateTime ClassDateCreated { get; set; }
        public DateTime TrackingDate { get; set; }
    }
    public class V_USER_TRACKED_Details : BusinessObject
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public List<DateTime> TrackingDate { get; set; }
    }
}
