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
            bool result = true;   
            result = new TB_SCHEDULE_DETAILSSql().DeleteByField("ScheduleDetailScheduleId", schedulesId);
            if (result)
            {
                result = new TB_SCHEDULESSql().Delete(schedulesId);
            }
            return result;
        }
        public TB_SCHEDULES GetById(int schedulesId)
        {
            return new TB_SCHEDULESSql().SelectByPrimaryKey(schedulesId);
        }
        public List<TB_SCHEDULES> GetAll()
        {
            return new TB_SCHEDULESSql().SelectAll();
        }
        // lay danh sach lop hoc cua giao vien , hoc sinh 
        public List<V_SCHEDULE_DETAILS> GetInfoClassBy(string userId , string userType,int pageNumber, int pageSize, out int count)
        {
            object cTemp;
            List<V_SCHEDULE_DETAILS> list = new List<V_SCHEDULE_DETAILS>();
            list = new V_SCHEDULE_DETAILSSql().SelectFromStoreOutParam(AppSettingKeys.GET_INFO_CLASS_BY, out cTemp, userId, userType, pageNumber,pageSize);
            count = (int)cTemp;
            return list;
        }
        public List<V_SHEDULES_CLASS> GetInfoClassDetails(string userId, string userType, int pageNumber, int pageSize, out int count)
        {
            List<V_SCHEDULE_DETAILS> list = new List<V_SCHEDULE_DETAILS>();
            List<V_SHEDULES_CLASS> result = new List<V_SHEDULES_CLASS>();
            object cTemp;
            list = new V_SCHEDULE_DETAILSSql().SelectFromStoreOutParam(AppSettingKeys.GET_INFO_CLASS_BY, out cTemp, userId, userType, pageNumber, pageSize);
            count = (int)cTemp;
            result = list.GroupBy(x => x.ScheduleId).Select(y => new V_SHEDULES_CLASS
            {
                ScheduleId = y.Key,
                ScheDuleDetails = y.Where(t => t.ScheduleId == y.Key).ToList()
            }).ToList();

            return result;
        }

        public List<V_NUMBER_STUDIES> GetCountStudieInClass(string scheduleId)
        {
            List<V_NUMBER_STUDIES> list = new List<V_NUMBER_STUDIES>();
            list = new V_NUMBER_STUDIESSql().SelectFromStore(AppSettingKeys.GET_COUNT_STUDIES_CLASS, scheduleId);
            return list;
        }

    }
}
