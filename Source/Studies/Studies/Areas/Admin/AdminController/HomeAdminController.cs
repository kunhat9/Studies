using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Controllers;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class HomeAdminController : BaseController
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}