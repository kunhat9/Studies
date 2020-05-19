using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_TRACKINGSFactory
    {
        public bool Insert(TB_TRACKINGS tracking)
        {
            return new TB_TRACKINGSSql().Insert(tracking);
        }
        public bool Update(TB_TRACKINGS tracking)
        {
            return new TB_TRACKINGSSql().Update(tracking);
        }
        public bool Delete(int trackingId)
        {
            return new TB_TRACKINGSSql().Delete(trackingId);
        }
        public TB_TRACKINGS GetById(int trackingId)
        {
            return new TB_TRACKINGSSql().SelectByPrimaryKey(trackingId);
        }

        public List<TB_TRACKINGS> GetAll()
        {
            return new TB_TRACKINGSSql().SelectAll();
        }
    }
}
