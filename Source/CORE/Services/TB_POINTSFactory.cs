using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_POINTSFactory
    {
        public bool Insert(TB_POINTS point)
        {
            return new TB_POINTSSql().Insert(point);
        }
        public bool Update(TB_POINTS point)
        {
            return new TB_POINTSSql().Update(point);
        }
        public bool Delete(int pointId)
        {
            return new TB_POINTSSql().Delete(pointId);
        }
        public List<TB_POINTS> GetAll()
        {
            return new TB_POINTSSql().SelectAll();
        }
        public TB_POINTS GetById(int pointId)
        {
            return new TB_POINTSSql().SelectByPrimaryKey(pointId);
        }
    }
}
