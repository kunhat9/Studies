using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_BOX_SUBJECTS : BusinessObject
    {
        public enum TB_BOX_SUBJECTS_Field
        {
            BoxSubjectId,
            BoxSubjectBoxId,
            BoxSubjectSubjectId

        }
        [PrimaryKey]
        public int BoxSubjectId { get; set; }
        public int BoxSubjectBoxId { get; set; }
        public int BoxSubjectSubjectId { get; set; }
        public TB_BOX_SUBJECTS() { }

    }
}
