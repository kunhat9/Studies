using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Helpers;
using CORE.Tables;

namespace WebAdmin.Controllers
{
    public class ClassController : BaseController
    {
        // GET: Class
        public ActionResult Index()
        {
            var classes = new List<TB_SCHEDULES>();
            try
            {
                classes = Schedules_Service.GetAll();
            }
            catch (Exception Ex)
            {
                IOHelper.WriteLog(StartUpPath, IpAddress, "Class:", Ex.Message, Ex.ToString());

            }
            return View(classes);
        }
    }
}