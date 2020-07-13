using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Views;
using WebAdmin.Controllers;
using CORE.Tables;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class ReportController : BaseController
    {
        // bao cao thong ke 
        public ActionResult Index()
        {

            return View();
        }

        public PartialViewResult _Index(string start = "", string end = "", string type = "DAY")
        {
            var report = new List<V_ReportChart>();
            try
            {
                report = ReportService.GetReportRevenueChart(start, end, type);
            }
            catch (Exception ex)
            {
                CORE.Helpers.IOHelper.WriteLog(StartUpPath, "ReportController :", ex.Message, ex.ToString());

            }
            return PartialView(report);
        }

        public ActionResult ReportSalary()
        {
            List<TB_SCHEDULES> list = new List<TB_SCHEDULES>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            try
            {
                list = Schedules_Service.GetAll();
                listUser = User_Service.GetAllTeacher();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.Schedule = list;
            ViewBag.User = listUser;
            return View();
        }
        public PartialViewResult _ReportSalary(string userId = "", string scheduleId = "", string startDate = "", string endDate = "", int pageNumber = 1, int pageSize = 10)
        {
            int count = 0;
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.maxNumber = 0;
            List<V_SALARY_TEACHER> list = new List<V_SALARY_TEACHER>();
            List<TB_SCHEDULES> listSche = new List<TB_SCHEDULES>();
            List<TB_SUBJECTS> listSuject = new List<TB_SUBJECTS>();
            List<TB_BOX_SUBJECTS> listBoxSubject = new List<TB_BOX_SUBJECTS>();
            List<TB_TRANSACTION> listTran = new List<TB_TRANSACTION>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            int total = 0;
            try
            {
                listUser = User_Service.GetAllTeacher();
                listTran = Transaction_Service.GetAllBy(userId, startDate, endDate, "TEACHER");
                listSuject = Subjects_Service.GetAll();
                listBoxSubject = Subjects_Boxes_Service.GetAll();
                listSche = Schedules_Service.GetAll();
                list = User_Service.GetSalaryTeacher(userId, scheduleId, startDate, endDate, pageNumber, pageSize, out count);
                total = count + listTran.Count;
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.maxNumber = Math.Ceiling((double)total / pageSize);
            ViewBag.Salary = list;
            ViewBag.Subject = listSuject;
            ViewBag.SubjectBox = listBoxSubject;
            ViewBag.Schedule = listSche;
            ViewBag.Transaction = listTran;
            ViewBag.User = listUser;
            return PartialView();
        }
        public ActionResult Tracking()
        {
            List<TB_SCHEDULES> list = new List<TB_SCHEDULES>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            try
            {
                list = Schedules_Service.GetAll();
                listUser = User_Service.GetAllTeacher();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.Schedule = list;
            ViewBag.User = listUser;
            return View();
        }
        public PartialViewResult _Tracking(string scheduleId = "", string startDate = "", string endDate = "", int pageNumber = 1, int pageSize = 10)
        {
            int count = 0;
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.maxNumber = 0;
            List<V_TRACKING_USER_CLASS> list = new List<V_TRACKING_USER_CLASS>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            List<TB_SCHEDULES> listSchedules = new List<TB_SCHEDULES>();
            List<DateTime> listDate = new List<DateTime>();
            List<V_TRACKING_SCHEDULE> a = new List<V_TRACKING_SCHEDULE>();
            try
            {
                a = Trackings_Service.GetTotalTrackingBy(scheduleId, startDate, endDate);
                listDate = GetDateTimeToMonth(startDate, endDate);
                listUser = User_Service.GetAllStudies();
                listSchedules = Schedules_Service.GetAll();
               
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.ListDate = listDate;
            ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);
            ViewBag.User = listUser;
            ViewBag.Schedule = listSchedules;
            return PartialView(a);
        }

        private List<DateTime> GetDateTimeToMonth(string startDate, string endDate)
        {
            List<DateTime> listDate = new List<DateTime>();
            DateTime now = DateTime.Now;
            DateTime parse, from, to;
            if (!string.IsNullOrEmpty(endDate)
                && DateTime.TryParseExact(endDate, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out parse))
            {
                to = parse;
            }
            else
            {
                to = now;
            }
            if (!string.IsNullOrEmpty(startDate)
                && DateTime.TryParseExact(startDate, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out parse)
                && parse <= to)
            {
                from = parse;
            }
            else
            {
                from = to.AddDays(-29);
            }
            while (Convert.ToInt32(from.ToString("yyyyMMdd")) <= Convert.ToInt32(to.ToString("yyyyMMdd")))
            {
                listDate.Add(from);
                from = from.AddDays(1);
            }
            return listDate;
        }
        public ActionResult TrackingDetails(string scheduledId="", string startDate="", string endDate="", string userId = "")
        {
            string url = Request.RawUrl;
            string query = Request.Url.Query;
            scheduledId = Request.QueryString["scheduledId"];
            List<DateTime> listDate = new List<DateTime>();
            
            List<TB_USERS> listUser = new List<TB_USERS>();
            List<TB_SCHEDULES> listSchedule = new List<TB_SCHEDULES>();
            List<V_TRACKING_USER_CLASS> list = new List<V_TRACKING_USER_CLASS>();
            try
            {
                listDate = GetDateTimeToMonth(startDate, endDate);
                listSchedule = Schedules_Service.GetAll();
                list = Trackings_Service.GetTrackingUser(userId, scheduledId, startDate, endDate);
                listUser = User_Service.GetAllStudies();
            }
            catch(Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.ListDate = listDate;
            ViewBag.Schedule = listSchedule;
            ViewBag.User = listUser;
            ViewBag.Url = url;
            return View(list);
        }
        public PartialViewResult _TrackingDetails(string scheduledId = "", string startDate = "", string endDate = "", string userId = "")
        {
            List<DateTime> listDate = new List<DateTime>();
            List<TB_SCHEDULES> details = new List<TB_SCHEDULES>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            List<V_TRACKING_USER_CLASS> list = new List<V_TRACKING_USER_CLASS>();
            try
            {
                listDate = GetDateTimeToMonth(startDate, endDate);
                listUser = User_Service.GetAllStudies();
                details = Schedules_Service.GetAll();
                list = Trackings_Service.GetTrackingUser(userId, scheduledId, String.Join("", startDate.Split('/')) , String.Join("", endDate.Split('/')));
            }
            catch(Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.Id = scheduledId;
            ViewBag.User = listUser;
            ViewBag.Schedule = details;
            ViewBag.ListDate = listDate;
            return PartialView(list);

        }

        public ActionResult Tuition()
        {
            List<TB_SCHEDULES> list = new List<TB_SCHEDULES>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            try
            {
                list = Schedules_Service.GetAll();
                listUser = User_Service.GetAllStudies();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.Schedule = list;
            ViewBag.User = listUser;
            return View();
        }
        public PartialViewResult _Tuition(string userId = "", string scheduleId = "", string startDate = "", string endDate = "", int pageNumber = 1, int pageSize = 10)
        {
            int count = 0;
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.maxNumber = 0;
            List<V_REPORT_TUITION> list = new List<V_REPORT_TUITION>();
            List<TB_SCHEDULES> listSche = new List<TB_SCHEDULES>();
            List<TB_SUBJECTS> listSuject = new List<TB_SUBJECTS>();
            List<TB_BOX_SUBJECTS> listBoxSubject = new List<TB_BOX_SUBJECTS>();
            List<TB_TRANSACTION> listTran = new List<TB_TRANSACTION>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            int total = 0;
            try
            {
                listUser = User_Service.GetAllStudies();
                listTran = Transaction_Service.GetAllBy(userId,startDate,endDate,"STUDIES");
                listSuject = Subjects_Service.GetAll();
                listBoxSubject = Subjects_Boxes_Service.GetAll();
                listSche = Schedules_Service.GetAll();
                list = User_Service.GetReportTuition(userId, scheduleId, startDate, endDate, pageNumber, pageSize, out count);
                total = count + listTran.Count;
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.maxNumber = Math.Ceiling((double)total / pageSize);
            ViewBag.Tuition = list;
            ViewBag.Subject = listSuject;
            ViewBag.SubjectBox = listBoxSubject;
            ViewBag.Schedule = listSche;
            ViewBag.Transaction = listTran;
            ViewBag.User  = listUser;
            return PartialView();
        }

        public ActionResult Schedules()
        {
            List<TB_USERS> list = new List<TB_USERS>();
            try
            {
                list = User_Service.GetAllTeacher();
            }catch(Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.Teacher = list;
            return View();
        }
        public PartialViewResult _Schedules(string userId)
        {
            List<V_SCHEDULE_DETAILS> list = new List<V_SCHEDULE_DETAILS>();
            int count = 0;
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    list = Schedules_Service.GetInfoClassBy(userId, "TEACHER", 1, short.MaxValue, out count);
                }
                
            }catch(Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            return PartialView(list);
        }

    }
}