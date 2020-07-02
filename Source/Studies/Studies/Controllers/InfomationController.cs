using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Tables;
using CORE.Views;
using WebAdmin.AppSession;
using WebAdmin.Models;

namespace WebAdmin.Controllers
{
    public class InfomationController : BaseController
    {
        // GET: Infomation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Schedules()
        {

            List<V_SCHEDULE_DETAILS> list = new List<V_SCHEDULE_DETAILS>();
            var user = (TB_USERS)Session[AppSessionKeys.USER_INFO];
            int count = 0;
            List<TB_USERS> listUser = new List<TB_USERS>();
            try
            {
                if (user.UserType.Equals("STUDIES"))
                {
                    list = Schedules_Service.GetInfoClassBy(user.UserId.ToString(), "STUDIES", 1, short.MaxValue, out count);
                }
                else
                {
                    list = Schedules_Service.GetInfoClassBy(user.UserId.ToString(), "TEACHER", 1, short.MaxValue, out count);
                }
                listUser = User_Service.GetAll();

            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            ViewBag.Type = string.IsNullOrEmpty(user.UserType) ? "" : user.UserType;
            ViewBag.User = listUser;
            return View(list);
        }

        public ActionResult RollCall()
        {
            List<V_TRACKING_USER_CLASS> list = new List<V_TRACKING_USER_CLASS>();
            List<TB_SCHEDULES> listSchedules = new List<TB_SCHEDULES>();
            try
            {
                listSchedules = Schedules_Service.GetAll();
                var user = (TB_USERS)Session[AppSessionKeys.USER_INFO];
                list = Trackings_Service.GetTrackingUser(user.UserId.ToString(), null, null, null);

            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            ViewBag.Schedules = listSchedules;
            return View(list);
        }

        public ActionResult Tuition()
        {

            List<V_TuitionStudies> listTuition = new List<V_TuitionStudies>();
            List<V_SALARY_TEACHER> listSalary = new List<V_SALARY_TEACHER>();
            var user = (TB_USERS)Session[AppSessionKeys.USER_INFO];
            List<TB_SCHEDULES> list = new List<TB_SCHEDULES>();
            List<TB_SUBJECTS> listSuject = new List<TB_SUBJECTS>();
            List<TB_BOX_SUBJECTS> listBoxSubject = new List<TB_BOX_SUBJECTS>();
            try
            {
                listSuject = Subjects_Service.GetAll();
                listBoxSubject = Subjects_Boxes_Service.GetAll();
                int count = 0;
                int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                DateTime timeTo = DateTime.Now;
                DateTime timeFrom = DateTime.Now.AddDays(DateTime.Now.Day - days);
                list = Schedules_Service.GetAll();
                if (user.UserType.Equals("STUDIES"))
                {

                    listTuition = User_Service.GetTuiTionStudies(user.UserId.ToString(), "", timeFrom.ToString("yyyy-MM-dd"), timeTo.ToString("yyyy-MM-dd"), 1, short.MaxValue, out count);

                }
                else
                {

                    listSalary = User_Service.GetSalaryTeacher(user.UserId.ToString(), "", timeFrom.ToString("yyyy-MM-dd"), timeTo.ToString("yyyy-MM-dd"), 1, short.MaxValue, out count);
                }



            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }

            ViewBag.Type = string.IsNullOrEmpty(user.UserType) ? "" : user.UserType;
            ViewBag.Subject = listSuject;
            ViewBag.SubjectBox = listBoxSubject;
            ViewBag.Schedule = list;
            ViewBag.Tuititon = listTuition;
            ViewBag.Salary = listSalary;
            
            return View();

        }

        public ActionResult Score()
        {
            List<TB_POINTS> list = new List<TB_POINTS>();
            List<ScoreModel> points = new List<ScoreModel>();
            try
            {
                list = Points_Service.GetAll();

                foreach (var tmp in list)
                {
                    bool check = false;
                    foreach (var point in points)
                    {

                        if (point.PointClassId.Equals(tmp.PointClassId))
                        {

                            check = true;
                            point.PointNumber.Add(tmp.PointNumber);

                        }

                    }

                    if (!check)
                    {
                        points.Add(new ScoreModel() { PointClassId = tmp.PointClassId, PointNumber = new List<decimal>() { tmp.PointNumber } });
                    }

                }

            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View(points);
        }

        public ActionResult Tracking()
        {
            List<V_SCHEDULE_DETAILS> list = new List<V_SCHEDULE_DETAILS>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            ViewBag.pageNumber = 1;
            ViewBag.pageSize = 10;
            ViewBag.maxNumber = 0;
            int count = 0;
            try
            {
                ViewBag.boxSubjects = Subjects_Boxes_Service.GetAll();
                TB_USERS user = (TB_USERS)Session[AppSessionKeys.USER_INFO];
                list = Schedules_Service.GetInfoClassBy(user.UserId.ToString(),user.UserType,1,short.MaxValue,out count);
                listUser = User_Service.GetAllTeacher();
                ViewBag.maxNumber = Math.Ceiling((double)count / 10);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.ToString();
            }
            ViewBag.Schedule = list;
            ViewBag.users = listUser;
            return View(list);
        }
        public ActionResult TrackingDetails(string scheduleId="", string createdDate = "")
        {
            List<V_SCHEDULE_DETAILS> details = new List<V_SCHEDULE_DETAILS>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            List<TB_USERS> listTeacher = new List<TB_USERS>();
            List<V_USER_TRACKED> listTracked = new List<V_USER_TRACKED>();
            int count = 0;
            try
            {
                if (string.IsNullOrEmpty(createdDate))
                {
                    createdDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                listTeacher = User_Service.GetAllTeacher();
                listUser = User_Service.GetStudiesBySchedule(scheduleId, 1, short.MaxValue, out count);
                details = Schedules_Service.GetInfoClassBy("", "STUDIES", 1, short.MaxValue, out count).Where(x => x.ScheduleId == Int32.Parse(scheduleId)).ToList();
                listTracked = User_Service.GetUserTracked(scheduleId, createdDate, 1, short.MaxValue, out count);
            }
            catch (Exception e)
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
            return View();
        }
    }
}