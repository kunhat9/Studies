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
            HomePage homePage = new HomePage();
            var teachers = new List<TB_USERS>();
            try
            {
                homePage = ReportService.GetHomePage();
                var users = User_Service.GetAll();

                foreach (var user in users)
                {
                    if (user.UserType.Equals("TEACHERS") && user.UserStatus.Equals("D"))
                    {
                        teachers.Add(user);
                    }
                }

            }
            catch (Exception ex)
            {
                CORE.Helpers.IOHelper.WriteLog(StartUpPath, "HomeController :", ex.Message, ex.ToString());

            }

            ViewBag.teachers = teachers;
            return View(homePage);
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