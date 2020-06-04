using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class UserController : Controller
    {
        // GET: Admin/User
        // quan ly thong tin hoc sinh , thong tin giao vien
        public ActionResult Index()
        {
            return View();
        }
    }
}