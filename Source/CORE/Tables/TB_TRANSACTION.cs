using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_TRANSACTION : BusinessObject
    {
        public int TransId { get; set; }
        public decimal TransNumber { get; set; }
        public string TransType { get; set; }
        public string TransNote { get; set; }
        public int TransUserId { get; set; }
        public DateTime TransDateCreated { get; set; }
        public DateTime TransBeginTime { get; set; }
        public DateTime TransEndTime { get; set; }
        public TB_TRANSACTION() { }
    }
}
