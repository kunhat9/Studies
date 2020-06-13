using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Controllers;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class ClassController : BaseController
    {
        // GET: Admin/Class
        // quan ly lop hoc 
        public ActionResult Index()
        {
            return View();
        }
    }
}