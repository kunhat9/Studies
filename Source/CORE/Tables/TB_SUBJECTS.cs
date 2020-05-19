using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_SUBJECTS : BusinessObject
    {
        
        [PrimaryKey]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public TB_SUBJECTS() { }

    }
}
