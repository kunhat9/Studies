using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Tables;
using CORE.Views;
using WebAdmin.AppSession;
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
        
        [ChildActionOnly]
        public PartialViewResult _HomeHeader()
        {
            if (Session[AppSessionKeys.USER_INFO] != null)
            {
                ViewBag.userName = ((TB_USERS) Session[AppSessionKeys.USER_INFO]).UserFullName;
            }
            return PartialView();
        }
        

        [ChildActionOnly]
        public PartialViewResult _HomeMenuLeft()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _Pagination(int maxNumber, int pageNumber)
        {
            ViewBag.maxNumber = maxNumber;
            ViewBag.pageNumber = pageNumber;
            return PartialView();
        }
    }
}