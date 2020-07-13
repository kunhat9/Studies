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
    public class TB_USERSFactory
    {
        public bool Insert(TB_USERS user)
        {
            return new TB_USERSSql().Insert(user);
        }
        public bool Update(TB_USERS user)
        {
            return new TB_USERSSql().Update(user);
        }
        public bool Delete(int userId)
        {
            return new TB_USERSSql().Delete(userId);
        }
        public TB_USERS GetById(int userId)
        {
            return new TB_USERSSql().SelectByPrimaryKey(userId);
        }
        public List<TB_USERS> GetAll()
        {
            return new TB_USERSSql().SelectAll();
        }
        public List<TB_USERS> GetAllStudies()
        {
            return new TB_USERSSql().FilterByField("UserType", "STUDIES");
        }
        public List<TB_USERS> GetAllTeacher()
        {
            return new TB_USERSSql().FilterByField("UserType", "TEACHER");
        }
        // LAY DANH SACH THONG TIN HOC SINH CUA 1 LOP HOC 
        public List<TB_USERS> GetStudiesBySchedule(string scheduleId, int pageNumber, int pageSize, out int count)
        {
            object cTemp;
            List<TB_USERS> list = new List<TB_USERS>();
            list = new TB_USERSSql().SelectFromStoreOutParam(AppSettingKeys.GET_STUDIES_BY_SCHEDULES, out cTemp, scheduleId, pageNumber, pageSize);
            count = (int)cTemp;
            return list;
        }
        // login vao webclient
        public V_INFO_LOGIN_CLIENT CheckLogin(string userName, string passWord, string type)
        {
            string ecode, edesc;
            List<TB_USERS> list = new List<TB_USERS>();
            list = new TB_USERSSql().SelectFromStoreOutEcode(out ecode, out edesc, AppSettingKeys.CHECK_LOGIN_CLIENT, userName, passWord, type);
            if (list == null)
            {
                list = new List<TB_USERS>();
            }
            V_INFO_LOGIN_CLIENT result = new V_INFO_LOGIN_CLIENT();
            result = list.Select(x => new V_INFO_LOGIN_CLIENT
            {
                ecode = ecode,
                edesc = edesc,
                user = list.FirstOrDefault()
            }).FirstOrDefault();
            return result;
        }
        // tinh luogn cho giao vien
        public List<V_SALARY_TEACHER> GetSalaryTeacher(string userId, string scheduleId, string startDate, string endDate, int pageNumber, int pageSize, out int count)
        {
            object cTemp;
            List<V_SALARY_TEACHER> list = new V_SALARY_TEACHERSql().SelectFromStoreOutParam(AppSettingKeys.GET_SALARY_TEACHER, out cTemp, userId, scheduleId, startDate, endDate, pageNumber, pageSize);
            count = (int)cTemp;
            return list;
        }

        public List<TB_USERS> GetAllBy(string keyText, string status, string scheduleId, string type, int pageNumber, int pageSize, out int count)
        {
            object cTemp;
            List<TB_USERS> list = new List<TB_USERS>();
            list = new TB_USERSSql().SelectFromStoreOutParam(AppSettingKeys.GET_ALL_USER_BY, out cTemp, keyText, status, scheduleId, type, pageNumber, pageSize);
            count = (int)cTemp;
            return list;
        }
        // bao cao hoc phi hoc sinh 
        public List<V_REPORT_TUITION> GetReportTuition(string userId, string scheduleId, string startDate, string endDate, int pageNumber, int pageSize, out int count)
        {
            object cTemp;
            List<V_REPORT_TUITION> list = new V_REPORT_TUITIONSql().SelectFromStoreOutParam(AppSettingKeys.GET_REPORT_TUITION, out cTemp, userId, scheduleId, startDate, endDate, pageNumber, pageSize);
            count = (int)cTemp;
            return list;
        }


        // lay danh danh thoi khoa bieu hoc sinh
        public List<V_TKBStudies> GetThoiKhoaBieu(string userId)
        {
            return new V_TKBStudiesSql().SelectFromStore(AppSettingKeys.GET_THOI_KHOA_BIEU, userId);
        }
        // lay danh sach hoc phi cua hoc sinh 
        public List<V_TuitionStudies> GetTuiTionStudies(string userId, string scheduleId, string startDate, string endDate, int pageNumber, int pageSize, out int count)
        {
            object cTemp;
            List<V_TuitionStudies> list = new List<V_TuitionStudies>();
            list = new V_TuitionStudiesSql().SelectFromStoreOutParam(AppSettingKeys.GET_TUITION_STUDIES, out cTemp, userId, scheduleId, startDate, endDate, pageNumber, pageSize);
            count = (int)cTemp;
            return list;
        }
        public List<V_USER_TRACKED_Details> GetUserTracked(string schedulesId, string numberMonth, string numberDayOfWeek, int pageNumber, int pageSize, out int count)
        {
            object cTemp;
            List<V_USER_TRACKED> list = new List<V_USER_TRACKED>();
            list = new V_USER_TRACKEDSql().SelectFromStoreOutParam(AppSettingKeys.GET_USER_TRACKED, out cTemp, schedulesId, numberMonth, numberDayOfWeek, pageNumber, pageSize);
            count = (int)cTemp;

            List<V_USER_TRACKED_Details> result = new List<V_USER_TRACKED_Details>();

            result = list.GroupBy(x => x.UserId).Select(y => new V_USER_TRACKED_Details
            {
                UserId = y.Key,
                UserFullName = y.Where(t => t.UserId == y.Key).FirstOrDefault().UserFullName,
                TrackingDate = y.Select(k => k.TrackingDate).ToList()

            }).ToList();
            return result;
        }

        public List<TB_USERS> GetTeacherByDateWeekTime(string dayOfWeek, string timeFrom, string timeTo)
        {
            return new TB_USERSSql().SelectFromStore(AppSettingKeys.GET_TEACHER_WEEK_TIME, dayOfWeek, timeFrom, timeTo);
        }

        public List<TB_USERS> GetUserRegister(string scheduleId)
        {
            return new TB_USERSSql().SelectFromStore(AppSettingKeys.GET_USER_REGISTER, scheduleId);
        }

        // bao caoo excel chi phi hoc sinh
        public List<V_REPORT_EXCEL_STUDIES> ReportTutionStudies(string userId, string startDate, string endDate)
        {
            string ecode ="", edesc= "";
            List<V_REPORT_EXCEL_STUDIES> list = new List<V_REPORT_EXCEL_STUDIES>();
            list =  new V_REPORT_EXCEL_STUDIESSql().SelectFromStoreOutEcode( out ecode, out edesc, AppSettingKeys.EXPORT_ORDER_TUITION, userId, startDate, endDate, "STUDIES");
            if (ecode.Equals("00"))
            {
                return list;
            }else
            {
                return new List<V_REPORT_EXCEL_STUDIES>();
            }
        }
        // bao cao excel luong giao vien 
        public string ReportSalaryTeacher(string userId, string startDate, string endDate)
        {
            
            string ecode = "", edesc = "";
            List<V_SALARY_TEACHER> list = new List<V_SALARY_TEACHER>();
            list = new V_SALARY_TEACHERSql().SelectFromStoreOutEcode(out ecode, out edesc, AppSettingKeys.EXPORT_ORDER_TUITION, userId, startDate, endDate, "TEACHER");
            return ecode;
        }
    }
}
