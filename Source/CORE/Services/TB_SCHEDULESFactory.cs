using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_SCHEDULESFactory
    {
        public bool Insert(TB_SCHEDULES schedules)
        {
            return new TB_SCHEDULESSql().Insert(schedules);
        }
        public bool Update(TB_SCHEDULES schedules)
        {
            return new TB_SCHEDULESSql().Update(schedules);
        }
        public bool Delete(int schedulesId)
        {
            return new TB_SCHEDULESSql().Delete(schedulesId);
        }
        public TB_SCHEDULES GetById(int schedulesId)
        {
            return new TB_SCHEDULESSql().SelectByPrimaryKey(schedulesId);
        }
        public List<TB_SCHEDULES> GetAll()
        {
            return new TB_SCHEDULESSql().SelectAll();
        }
    }
}
