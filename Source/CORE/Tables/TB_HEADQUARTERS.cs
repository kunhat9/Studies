using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_HEADQUARTERS : BusinessObject
    {
        [PrimaryKey]
        public int HeadQuarterId { get; set; }
        public string HeadQuarterName { get; set; }
        public string HeadQuarterAddress { get; set; }
        public string HeadQuarterPhone { get; set; }
        public string HeadQuarterEmail { get; set; }
        public string HeadQuarterNote { get; set; }
        public string HeadQuarterLat { get; set; }
        public string HeadQuarterLong { get; set; }
        public TB_HEADQUARTERS() { }

    }
}
