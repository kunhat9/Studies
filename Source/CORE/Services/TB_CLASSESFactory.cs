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
        public List<V_CLASS> GetClassBy(string keyText, string boxId, string subjectId, string timeIn, string timeEnd, string status, int pageNumber, int pageSize, out int count)
        {
            List<V_CLASS> list = new List<V_CLASS>();
            object cTemp;
            list = new V_CLASSSql().SelectFromStoreOutParam(AppSettingKeys.GET_CLASS_BY, out cTemp, keyText, boxId, subjectId, timeIn, timeEnd, status, pageNumber, pageSize);
            count = (int)cTemp;
            return list;
        }
        public bool InsertOrUpdateClassFromAdmin(string scheduleId, string boxId,string price, string startDate, string endDate, string subjectId, string dayOfWeek, string timeIn , string timeEnd, string status, string userId,string note)
        {
            string ecode, edesc;
            TB_CLASSESSql sql = new TB_CLASSESSql();
            sql.SelectFromStore(out ecode, out edesc,AppSettingKeys.INSERT_OR_UPDATE_CLASS_FROM_ADMIN, scheduleId,boxId,price,startDate,endDate, subjectId, dayOfWeek, timeIn, timeEnd, status, userId, note);          
            if (ecode.Equals("00"))
            {
                return true;
            }else
            {
                return false;
            }
        }

        public bool InsertStudiesToClass(int ScheduleId, List<string> Userids, string type)
        {
            string ecode, edesc;
            TB_CLASSESSql sql = new TB_CLASSESSql();
            string listUserId = String.Join(",", Userids);
            sql.SelectFromStore(out ecode, out edesc, AppSettingKeys.INSERT_STUDIES_TO_CLASS, ScheduleId, listUserId, type);
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
