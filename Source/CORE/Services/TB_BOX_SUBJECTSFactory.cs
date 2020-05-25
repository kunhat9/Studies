using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_BOX_SUBJECTSFactory
    {
        public bool InsertOrUpdate(int boxId, List<string> listSubjectId)
        {
            string ecode = null, edesc = null;
            new TB_BOX_SUBJECTSSql().SelectFromStore(out ecode,out edesc, AppSettingKeys.INSERT_OR_UPDATE_SUBJECT_TO_BOX, boxId, listSubjectId);
            if (ecode.Equals("00"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        



    }
}
