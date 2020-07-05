using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_BOX_SUBJECT : BusinessObject
    {
        [PrimaryKey]
        public int BoxSubjectId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public decimal BoxSubjectPrice { get; set; }
        public int BoxId { get; set; }
        public string BoxCode { get; set; }
    }
}
