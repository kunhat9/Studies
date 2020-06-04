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
        // LAY DANH SACH THONG TIN HOC SINH CUA 1 LOP HOC 
        public List<TB_USERS> GetStudiesBySchedule(int scheduleId, int pageNumber, int pageSize, out int count)
        {
            object cTemp;
            List<TB_USERS> list = new List<TB_USERS>();
            list = new TB_USERSSql().SelectFromStoreOutParam(AppSettingKeys.GET_STUDIES_BY_SCHEDULES, out cTemp, scheduleId, pageNumber, pageSize);
            count = (int)cTemp;
            return list;
        }
        // login vao webclient
        public V_INFO_LOGIN_CLIENT CheckLogin(string userName, string passWord)
        {
            string ecode, edesc;
            List<V_INFO_LOGIN_CLIENT> list = new List<V_INFO_LOGIN_CLIENT>();
            list = new V_INFO_LOGIN_CLIENTSql().SelectFromStoreOutEcode(out ecode, out edesc, AppSettingKeys.CHECK_LOGIN_CLIENT, userName, passWord);
            V_INFO_LOGIN_CLIENT reuslt = new V_INFO_LOGIN_CLIENT();
            reuslt = list.Select(x => new V_INFO_LOGIN_CLIENT
            {
                ecode = x.ecode,
                edesc = x.edesc,
                user = x.user
            }).FirstOrDefault();
            return reuslt;
        }
        // tinh luogn cho giao vien
        public List<V_SALARY_TEACHER> GetSalaryTeacher(int userId , int scheduleId , string startDate, string endDate , int pageNumber , int pageSize)
        {
            return new V_SALARY_TEACHERSql().SelectFromStore(AppSettingKeys.GET_SALARY_TEACHER, userId,scheduleId,startDate,endDate,pageNumber,pageSize);
        }
    }
}
