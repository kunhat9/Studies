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
            try
            {
                list = User_Service.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return View(list);
        }
    }
}