using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class SubjectController : Controller
    {
        // GET: Admin/Subject
        // thong tin mon hoc, lop khoii 
        public ActionResult Index()
        {
            return View();
        }
    }
}