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
            try
            {
                listSuject = Subjects_Service.GetAll();
                listBoxSubject = Subjects_Boxes_Service.GetAll();
                listSche = Schedules_Service.GetAll();
                list = User_Service.GetSalaryTeacher(userId, scheduleId, startDate, endDate, pageNumber, pageSize, out count);

            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);
            ViewBag.Salary = list;
            ViewBag.Subject = listSuject;
            ViewBag.SubjectBox = listBoxSubject;
            ViewBag.Schedule = listSche;
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
        public PartialViewResult _Tracking(string userId = "", string scheduleId = "", string startDate = "", string endDate = "", int pageNumber = 1, int pageSize = 10)
        {
            int count = 0;
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.maxNumber = 0;
            List<V_TRACKING_USER_CLASS> list = new List<V_TRACKING_USER_CLASS>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            List<TB_SCHEDULES> listSchedules = new List<TB_SCHEDULES>();
            try
            {
                listUser = User_Service.GetAllStudies();
                listSchedules = Schedules_Service.GetAll();
                list = Trackings_Service.GetTrackingUser(userId, scheduleId, startDate, endDate);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);
            ViewBag.User = listUser;
            ViewBag.Schedule = listSchedules;
            return PartialView(list);
        }
        public ActionResult TrackingDetails()
        {
            List<TB_SCHEDULES> list = new List<TB_SCHEDULES>();
            //List<V_SCHEDULE_DETAILS> list = new List<V_SCHEDULE_DETAILS>();      
            //int count = 0;
            try
            {
                list = Schedules_Service.GetAll();
                //list = Schedules_Service.GetInfoClassBy("","",1,short.MaxValue,out count);
                
            }catch(Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.Schedule = list;
            return View();
        }
        public PartialViewResult _TrackingDetails(string scheduleId, string createdDate)
        {
            List<V_SCHEDULE_DETAILS> details = new List<V_SCHEDULE_DETAILS>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            List<TB_USERS> listTeacher = new List<TB_USERS>();
            List<V_USER_TRACKED> listTracked = new List<V_USER_TRACKED>();
            int count = 0;
            try
            {
                listTeacher = User_Service.GetAllTeacher();
                listUser = User_Service.GetStudiesBySchedule(scheduleId,1,short.MaxValue,out count);
                details = Schedules_Service.GetInfoClassBy("","STUDIES",1,short.MaxValue,out count).Where(x=>x.ScheduleId == Int32.Parse(scheduleId)).ToList();
                listTracked = User_Service.GetUserTracked(scheduleId, createdDate, 1, short.MaxValue, out count);
            }
            catch(Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            List<TB_USERS> lst = new List<TB_USERS>();
            foreach (var item in listUser)
            {

                if (listTracked.Where(x => x.UserId == item.UserId).ToList().Count == 0)
                {
                    lst.Add(item);
                }
            }
            ViewBag.UserTracked = listTracked;
            ViewBag.Teacher = listTeacher;
            ViewBag.User = lst;
            ViewBag.Schedule = details;
            return PartialView();

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
            List<V_TuitionStudies> list = new List<V_TuitionStudies>();
            List<TB_SCHEDULES> listSche = new List<TB_SCHEDULES>();
            List<TB_SUBJECTS> listSuject = new List<TB_SUBJECTS>();
            List<TB_BOX_SUBJECTS> listBoxSubject = new List<TB_BOX_SUBJECTS>();
            try
            {
                listSuject = Subjects_Service.GetAll();
                listBoxSubject = Subjects_Boxes_Service.GetAll();
                listSche = Schedules_Service.GetAll();
                list = User_Service.GetTuiTionStudies(userId, scheduleId, startDate, endDate, pageNumber, pageSize, out count);

            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);
            ViewBag.Tuition = list;
            ViewBag.Subject = listSuject;
            ViewBag.SubjectBox = listBoxSubject;
            ViewBag.Schedule = listSche;
            return PartialView();
        }
    }
}