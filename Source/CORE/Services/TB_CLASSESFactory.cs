using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_CLASSESFactory
    {
        public bool Insert(TB_CLASSES classes)
        {
            return new TB_CLASSESSql().Insert(classes);
        }
        public bool Update(TB_CLASSES classes)
        {
            return new TB_CLASSESSql().Update(classes);
        }
        public bool Delete(int classId)
        {
            return new TB_CLASSESSql().Delete(classId);
        }
        public List<TB_CLASSES> GetAll()
        {
            return new TB_CLASSESSql().SelectAll();
        }
       public TB_CLASSES GetById(int classId)
        {
            return new TB_CLASSESSql().SelectByPrimaryKey(classId);
        }
    }
}
