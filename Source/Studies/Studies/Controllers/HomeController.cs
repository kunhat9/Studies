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

        public ActionResult Index(string error ="")
        {
            List<TB_SUBJECTS> list = new List<TB_SUBJECTS>();
            try
            {
                list = Subjects_Service.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            ViewBag.error = error;
            return View(list);
        }

        [ChildActionOnly]
        public PartialViewResult _HomeHeader()
        {
            List<TB_BOXES> lstBoxes = new List<TB_BOXES>();
            TB_USERS user = null;

            try
            {
                lstBoxes = Boxes_Service.GetAll();
                ViewBag.boxes = lstBoxes;
                
                if(Session[AppSessionKeys.USER_INFO] != null){
                    user = (TB_USERS)Session[AppSessionKeys.USER_INFO];
                    ViewBag.id = user.UserId;

                }
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
                V_INFO_LOGIN_CLIENT info = User_Service.CheckLogin(username, password);
                if (info == null)
                {
                    ViewBag.error = "Tài khoản hoặc mật khẩu không chính xác";
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

            return RedirectToAction("Index",new {@error=ViewBag.error});
        }
        [HttpPost]
        public ActionResult Register(String username,String password,String fullname,String phone,String confirmpassword)
        {
            try
            {

                bool check = true;
                if (!password.Equals(confirmpassword))
                {
                    ViewBag.error = "Mật khẩu không khớp";
                    check = false;
                }
                else if (username.Equals("") || fullname.Equals("") || password.Equals(""))
                {
                    ViewBag.error = "Vui lòng điền tất cả các trường";
                    check = false;
                }

            
                TB_USERS users = new TB_USERS()
                    {
                        UserName =  username,
                        UserPassword = password,
                        UserFullName = fullname,
                        UserPhone =  phone,
                        UserType = "STUDIES",
                        UserDateCreated = DateTime.Now
                    };
                check = User_Service.Insert(users);
                if (!check)
                {
                    ViewBag.error = "Đã có lỗi xảy ra. Vui lòng thử lại";
                }

                return RedirectToAction("Index",new {@error=ViewBag.error});
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