using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_CLASSES : BusinessObject
    {
        [PrimaryKey]
        public int ClassId { get; set; }
        public int ClassUserId { get; set; }
        public int ClassScheduleId { get; set; }
        public TB_CLASSES() { }

    }
}
