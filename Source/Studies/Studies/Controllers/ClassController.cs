using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Helpers;
using CORE.Views;
using CORE.Tables;
using WebAdmin.AppSession;

namespace WebAdmin.Controllers
{
    public class ClassController : BaseController
    {
        // GET: Class
        public ActionResult Index()
        {
            List<V_SHEDULES_CLASS> classes = new List<V_SHEDULES_CLASS>();
            List<TB_FILES> files = new List<TB_FILES>();
            List<V_NUMBER_STUDIES> listCount = new List<V_NUMBER_STUDIES>();
            int count = 0;
            try
            {
                listCount = Schedules_Service.GetCountStudieInClass("");
                files = Files_Service.GetAll();
                classes = Schedules_Service.GetInfoClassDetails("", "TEACHER", 1, short.MaxValue, out count);
            }
            catch (Exception Ex)
            {
                IOHelper.WriteLog(StartUpPath, IpAddress, "Class:", Ex.Message, Ex.ToString());

            }
            ViewBag.Count = listCount;
            ViewBag.File = files;
            return View(classes);
        }
        public ActionResult Detail(int scheduleId)
        {
            List<TB_FILES> files = new List<TB_FILES>();
            V_SHEDULES_CLASS classes = new V_SHEDULES_CLASS();
            List<TB_SUBJECTS> listSubject = new List<TB_SUBJECTS>();
            List<TB_BOXES> listBox = new List<TB_BOXES>();
            List<TB_BOX_SUBJECTS> listBoxSubject = new List<TB_BOX_SUBJECTS>();
            List<TB_USERS> listUser = new List<TB_USERS>();
            TB_USERS userLogin = (TB_USERS)Session[AppSessionKeys.USER_INFO_CLIENT];
            int count = 0;
            try
            {
                files = Files_Service.GetAll();
                listUser = User_Service.GetAllTeacher();
                listBox = Boxes_Service.GetAll();
                listSubject = Subjects_Service.GetAll();
                listBoxSubject = Subjects_Boxes_Service.GetAll();
                classes = Schedules_Service.GetInfoClassDetails("", "TEACHER", 1, short.MaxValue, out count).Where(x => x.ScheduleId == scheduleId).FirstOrDefault();
            }
            catch (Exception Ex)
            {
                IOHelper.WriteLog(StartUpPath, IpAddress, "Class:", Ex.Message, Ex.ToString());

            }
            ViewBag.UserLogin = userLogin;
            ViewBag.File = files;
            ViewBag.User = listUser;
            ViewBag.Box = listBox;
            ViewBag.Subject = listSubject;
            ViewBag.BoxSubject = listBoxSubject;

            return View(classes);
        }
    }
}