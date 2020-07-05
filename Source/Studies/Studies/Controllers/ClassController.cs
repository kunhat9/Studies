using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Helpers;
using CORE.Views;
using CORE.Tables;

namespace WebAdmin.Controllers
{
    public class ClassController : BaseController
    {
        // GET: Class
        public ActionResult Index()
        {
            List<V_SCHEDULE_DETAILS> classes = new List<V_SCHEDULE_DETAILS>();
            List<TB_FILES> files = new List<TB_FILES>();
            int count = 0;
            try
            {
                files = Files_Service.GetAll();
                classes = Schedules_Service.GetInfoClassBy("", "TEACHER", 1, short.MaxValue, out count);
            }
            catch (Exception Ex)
            {
                IOHelper.WriteLog(StartUpPath, IpAddress, "Class:", Ex.Message, Ex.ToString());

            }
            ViewBag.File = files;
            return View(classes);
        }
        public ActionResult Detail(int scheduleId)
        {
            List<TB_FILES> files = new List<TB_FILES>();
            V_SCHEDULE_DETAILS classes = new V_SCHEDULE_DETAILS();
            List<TB_SUBJECTS> listSubject = new List<TB_SUBJECTS>();
            List<TB_BOXES> listBox = new List<TB_BOXES>();
            List<TB_BOX_SUBJECTS> listBoxSubject = new List<TB_BOX_SUBJECTS>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            int count = 0;
            try
            {
                files = Files_Service.GetAll();
                listUser = User_Service.GetAllTeacher();
                listBox = Boxes_Service.GetAll();
                listSubject = Subjects_Service.GetAll();
                listBoxSubject = Subjects_Boxes_Service.GetAll();
                classes = Schedules_Service.GetInfoClassBy("", "TEACHER", 1, short.MaxValue, out count).Where(x=>x.ScheduleId == scheduleId).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                IOHelper.WriteLog(StartUpPath, IpAddress, "Class:", Ex.Message, Ex.ToString());

            }
            ViewBag.File = files;
            ViewBag.User = listUser;
            ViewBag.Box = listBox;
            ViewBag.Subject = listSubject;
            ViewBag.BoxSubject = listBoxSubject;

            return View(classes);
        }
    }
}