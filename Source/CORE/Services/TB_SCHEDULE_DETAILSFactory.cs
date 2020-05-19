using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_SCHEDULE_DETAILSFactory
    {
        public bool Insert(TB_SCHEDULE_DETAILS details)
        {
            return new TB_SCHEDULE_DETAILSSql().Insert(details);
        }
        public bool Update(TB_SCHEDULE_DETAILS details)
        {
            return new TB_SCHEDULE_DETAILSSql().Update(details);
        }
        public bool Delete(TB_SCHEDULE_DETAILS detailsId)
        {
            return new TB_SCHEDULE_DETAILSSql().Delete(detailsId);
        }
        public List<TB_SCHEDULE_DETAILS> GetAll()
        {
            return new TB_SCHEDULE_DETAILSSql().SelectAll();
        }
    }
}
