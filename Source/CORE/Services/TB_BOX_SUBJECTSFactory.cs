using CORE.Internal;
using CORE.Internal.ViewSql;
using CORE.Tables;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_BOX_SUBJECTSFactory
    {
        public bool InsertOrUpdate(int boxId, List<string> listSubjectId, string boxSubjectId)
        {
            string list = string.Join(",", listSubjectId);
            string ecode = null, edesc = null;
            new TB_BOX_SUBJECTSSql().SelectFromStore(out ecode,out edesc, AppSettingKeys.INSERT_OR_UPDATE_SUBJECT_TO_BOX, boxId, list, boxSubjectId);
            if (ecode.Equals("00"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public TB_BOX_SUBJECTS GetById(int boxSubjectId)
        {
            return new TB_BOX_SUBJECTSSql().SelectByPrimaryKey(boxSubjectId);
        }
        public List<TB_BOX_SUBJECTS> GetAll()
        {
            return new TB_BOX_SUBJECTSSql().SelectAll();
        }
      
        public List<V_BOX_SUBJECT> GetAllBy(string keyText, int pageNumber ,int pageSize, out int count)
        {
            object cTemp;
            List<V_BOX_SUBJECT> list = new List<V_BOX_SUBJECT>();
            list = new V_BOX_SUBJECTSql().SelectFromStoreOutParam(AppSettingKeys.GET_SUBJECT_BY, out cTemp, keyText, pageNumber, pageSize);
            count = (int)cTemp;
            return list;
        }
    }
}
