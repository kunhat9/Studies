using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CORE.Tables;
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
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _HomeHeader()
        {
            TB_USERS user = null;
            if(Session[AppSessionKeys.USER_INFO] != null){
                user = (TB_USERS)Session[AppSessionKeys.USER_INFO];
            }
            return PartialView(user);
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

        [HttpPost]
        public ActionResult Login(String username,String password)
        {
            try
            {
                Subject_Service.GetAll();
                V_INFO_LOGIN_CLIENT info = User_Service.CheckLogin(username, password);
                if (info == null)
                {
                    ViewBag.error = "Username or Password are incorrect";
                }
                else if (info.ecode.Equals("200"))
                {
                    Session[AppSessionKeys.USER_INFO] = info.user;
                }
                else
                {
                    ViewBag.error = info.edesc;
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            
            return View("Index");
        }
        [HttpPost]
        public ActionResult Register(String username,String password,String fullname,String phone,String confirmpassword)
        {
            try
            {
                V_INFO_LOGIN_CLIENT info = User_Service.CheckLogin(username, password);
                if (info == null)
                {
                    ViewBag.error = "Username or Password are incorrect";
                }
                bool check = true;
                if (!password.Equals(confirmpassword))
                {
                    ViewBag.error = "Password and confirm password must match";
                    check = false;
                }
                else if (username.Equals("") || fullname.Equals("") || password.Equals(""))
                {
                    ViewBag.error = "Please fill all the fields";
                    check = false;
                }

            
                TB_USERS users = new TB_USERS()
                    {
                        UserName =  username,
                        UserPassword = password,
                        UserFullName = fullname,
                        UserPhone =  phone,
                        UserType = "STUDIES"
                    };
                check = User_Service.Insert(users);
                if (!check)
                {
                    ViewBag.error = "An error occurred while processing your request";
                }

                return View("Index");
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return View("Index");
            }
        }
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