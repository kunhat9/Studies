using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class ClassController : Controller
    {
        // GET: Admin/Class
        // quan ly lop hoc 
        public ActionResult Index()
        {
            return View();
        }
    }
}