using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAdmin.Controllers
{
    public class InfomationController : Controller
    {
        // GET: Infomation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Schedules()
        {
            return View();
        }
        
        public ActionResult RollCall()
        {
            return View();
        }
        
        public ActionResult Tuition()
        {
            return View();
        }
        
        public ActionResult Score()
        {
            return View();
        }
    }
}