using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_SUBJECTSFactory
    {
        public bool Insert(TB_SUBJECTS subject)
        {
            return new TB_SUBJECTSSql().Insert(subject);
        }
        public bool Update(TB_SUBJECTS subject)
        {
            return new TB_SUBJECTSSql().Update(subject);
        }
        public bool Delete(int subjectId)
        {
            return new TB_SUBJECTSSql().Delete(subjectId);
        }
        public TB_SUBJECTS GetById(int subjectId)
        {
            return new TB_SUBJECTSSql().SelectByPrimaryKey(subjectId);
        }
        public List<TB_SUBJECTS> GetAll()
        {
            return new TB_SUBJECTSSql().SelectAll();
        }
        public List<TB_SUBJECTS> GetByBox(int boxId)
        {
            return new TB_SUBJECTSSql().SelectFromStore(AppSettingKeys.GET_SUBJECT_BY_BOX_ID,boxId);
        }
       
    }
}
