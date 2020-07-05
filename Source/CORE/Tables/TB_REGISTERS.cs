using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_REGISTERS : BusinessObject
    {
        [PrimaryKey]
        public int RegisterId { get; set; }
        public int RegisterUserId { get; set; }
        public int RegisterScheduleId { get; set; }
        public DateTime RegisterDateCreate { get; set; }
        public TB_REGISTERS() { }

    }
}
