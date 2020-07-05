using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_TEACHING_SCHEDULESFactory
    {
        public bool Insert(TB_TEACHING_SCHEDULES teaching)
        {
            return new TB_TEACHING_SCHEDULESSql().Insert(teaching);
        }
        public bool Update(TB_TEACHING_SCHEDULES teaching)
        {
            return new TB_TEACHING_SCHEDULESSql().Update(teaching);
        }
        public bool Delete(int teachingId)
        {
            return new TB_TEACHING_SCHEDULESSql().Delete(teachingId);
        }
        public TB_TEACHING_SCHEDULES GetById(int teachingId)
        {
            return new TB_TEACHING_SCHEDULESSql().SelectByPrimaryKey(teachingId);
        }
        public List<TB_TEACHING_SCHEDULES> GetAll()
        {
            return new TB_TEACHING_SCHEDULESSql().SelectAll();
        }
        public List<TB_TEACHING_SCHEDULES> GetByUserId(int userId)
        {
            return new TB_TEACHING_SCHEDULESSql().FilterByField("TeachingScheduleUserId", userId);
        }
        // đăng kí lịch dạy giáo viên
        public int InsertTeacherAndSchedule(TB_USERS user , List<TB_TEACHING_SCHEDULES> listSchedule)
        {
            string ecode, edesc;
            TB_TEACHING_SCHEDULESSql sql = new TB_TEACHING_SCHEDULESSql();
            string xmlUser = "<row>" +user.ToStringXml() +"</row>"; 
            string xml = "";
            foreach (var item in listSchedule)
            {
                xml += "<row>" + item.ToStringXml() + "</row>";
            }
            string xmllistSchedule = "<row>" + xml + "</row>";
            sql.SelectFromStore(out ecode, out edesc, AppSettingKeys.INSERT_TEACHER_AND_SCHEDULES, xmlUser, xmllistSchedule);
            if (ecode.Equals("00"))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
