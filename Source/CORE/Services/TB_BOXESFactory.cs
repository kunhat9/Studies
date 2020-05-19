using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_BOXESFactory
    {
        public bool Insert(TB_BOXES box)
        {
            return new TB_BOXESSql().Insert(box);
        }
        public bool Update(TB_BOXES box)
        {
            return new TB_BOXESSql().Update(box);
        }
        public bool Delete(int boxId)
        {
            return new TB_BOXESSql().Delete(boxId);
        }
        public TB_BOXES GetById(int boxId)
        {
            return new TB_BOXESSql().SelectByPrimaryKey(boxId);
        }
        public List<TB_BOXES> GetAll()
        {
            return new TB_BOXESSql().SelectAll();
        }
    }
}
