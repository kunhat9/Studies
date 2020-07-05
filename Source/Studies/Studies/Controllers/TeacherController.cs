using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Tables;

namespace WebAdmin.Controllers
{
    public class TeacherController : BaseController
    {
        // GET: Teacher
        public ActionResult Index()
        {
            List<TB_USERS> list = new List<TB_USERS>();
            List<TB_FILES> files = new List<TB_FILES>();
            try
            {
                files = Files_Service.GetAll();
                list = User_Service.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            ViewBag.File = files;
            return View(list);
        }
    }
}