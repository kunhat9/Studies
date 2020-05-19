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
        public string RegisterName { get; set; }
        public string RegisterPlace { get; set; }
        public string RegisterPhone { get; set; }
        public int RegisterBoxSubjectId { get; set; }
        public DateTime RegisterDateCreate { get; set; }
        public TB_REGISTERS() { }

    }
}
