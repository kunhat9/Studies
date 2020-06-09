using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAdmin.AppSession;

namespace WebAdmin.Controllers
{
    public class HomeController : BaseController
    {
       
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }

   

        #region OutLogin

        public ActionResult Index()
        {
            V_INFO_LOGIN_CLIENT list = User_Service.CheckLogin("ADMIN","1");
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _HomeHeader()
        {
            //ViewBag.userName = ((VIEW_INFO_USER_LOGIN)Session[AppSessionKeys.USER_INFO]).USER_LOGIN;
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _HomeFooter()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _HomeMenuLeft()
        {
            //ViewBag.boxName = ((VIEW_INFO_USER_LOGIN)Session[AppSessionKeys.USER_INFO]).BOX_NAME;
            //ViewBag.fullName = ((VIEW_INFO_USER_LOGIN)Session[AppSessionKeys.USER_INFO]).USER_FULLNAME;
            //List<SYS_ACTIONS> actions = ((List<SYS_ACTIONS>)Session[AppSessionKeys.LIST_ACTION]).Where(x => x.ACTION_ISSHOW).ToList();
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult _Pagination(int maxNumber, int pageNumber)
        {
            ViewBag.maxNumber = maxNumber;
            ViewBag.pageNumber = pageNumber;
            return PartialView();
        }

        #endregion

        public ActionResult NotPermission()
        {
            return View();
        }

        public PartialViewResult _NotPermission()
        {
            return PartialView();
        }
    }
}