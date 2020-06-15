using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Tables;
using WebAdmin.Configuration;
using WebAdmin.Controllers;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        // quan ly thong tin hoc sinh , thong tin giao vien
        public ActionResult Students()
        {
            return View();
        }
        
        [AjaxChildActionOnly]
        public PartialViewResult _Students(string keyText = "", int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.maxNumber = 0;
            var users = new List<TB_USERS>();
            try
            {
                int count = 0;
                users = User_Service.GetAllBy(keyText, null, null, "STUDIES", pageNumber, pageSize, out count);
                ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);
            }
            catch (Exception ex)
            {
                CORE.Helpers.IOHelper.WriteLog(StartUpPath, "UserController :", ex.Message, ex.ToString());
            }

            return PartialView(users);
        }
        [ChildActionOnly]
        public PartialViewResult _Students_Details(string userID)
        {
            TB_USERS user = new TB_USERS();

            try
            {
                user = User_Service.GetById(int.Parse(userID));
            }
            catch (Exception ex)
            {
                CORE.Helpers.IOHelper.WriteLog(StartUpPath, "UserController :", ex.Message, ex.ToString());
            }

            return PartialView(user);
        }
    }
}