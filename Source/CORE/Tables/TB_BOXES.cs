using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_BOXES : BusinessObject
    {
        [PrimaryKey]
        public int BoxId { get; set; }
        public string BoxCode { get; set; }
        public TB_BOXES() { }


    }
}
