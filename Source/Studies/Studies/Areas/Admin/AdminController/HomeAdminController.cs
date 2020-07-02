using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Tables;
using CORE.Views;
using WebAdmin.AppSession;
using WebAdmin.Controllers;
using WebAdmin.Cookie;

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
                ViewBag.userName = ((TB_USERS)Session[AppSessionKeys.USER_INFO]).UserFullName;
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


        public ActionResult Login(string url)
        {
            if (Session[AppSessionKeys.USER_INFO] != null)
            {
                if (string.IsNullOrEmpty(url))
                {
                    return RedirectToAction("/Index");
                }
                else
                {
                    return RedirectPermanent(url);
                }
            }
            else
            {
                ViewBag.username = AppCookieInfo.UserID;
                ViewBag.password = AppCookieInfo.HashedPassword;
                ViewBag.business = AppCookieInfo.LoginType;
            }
            return View();
        }
        #region
        [HttpPost]
        public ActionResult Login(string username, string password, string url, string remember)
        {
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.error = "Tên đăng nhập không được để trống";
                AppCookieInfo.UserID = "";
                AppCookieInfo.HashedPassword = "";
                return View();
            }
            else if (string.IsNullOrEmpty(password))
            {
                ViewBag.error = "Mật khẩu không được để trống";
                AppCookieInfo.UserID = "";
                AppCookieInfo.HashedPassword = "";
                return View();
            }

            V_INFO_LOGIN_CLIENT user;
            try
            {
                user = User_Service.CheckLogin(username, password,"ADMIN");
                
            }
            catch (Exception ex)
            {
                user = null;
                CORE.Helpers.IOHelper.WriteLog(StartUpPath, IpAddress, "Login :", ex.Message, ex.ToString());
            }

            if (user.ecode == "200")
            {
                Session[AppSessionKeys.USER_INFO] = user.user;

                if (remember == "on")
                {
                    AppCookieInfo.UserID = username;
                    AppCookieInfo.HashedPassword = password;
                }
                else
                {
                    AppCookieInfo.UserID = "";
                    AppCookieInfo.HashedPassword = "";
                }
            }
            else
            {
                ViewBag.error = "Đăng nhập không thành công";
                AppCookieInfo.UserID = "";
                AppCookieInfo.HashedPassword = "";
            }

            return Login(url);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("/Index");
        }
        #endregion
    }
}