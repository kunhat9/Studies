using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Tables;
using CORE.Views;
using WebAdmin.Controllers;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class SubjectController : BaseController
    {
        // GET: Admin/Subject
        // thong tin mon hoc, lop khoii 
        public ActionResult Index()
        {
            return View();
        }
        
        public PartialViewResult _Index(string keyText = "", int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.maxNumber = 0;
            var subjects = new List<V_BOX_SUBJECT>();
            try
            {
                int count = 0;
                subjects = Subjects_Boxes_Service.GetAllBy(keyText, pageNumber, pageSize, out count);
                ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);
            }
            catch (Exception ex)
            {
                CORE.Helpers.IOHelper.WriteLog(StartUpPath, "SubjectController :", ex.Message, ex.ToString());
            }

            return PartialView(subjects);
        }
        
        public PartialViewResult _Detail()
        {
            var subjects = new List<TB_SUBJECTS>();
            try
            {
                subjects = Subjects_Service.GetAll();
                ViewBag.boxes = Boxes_Service.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return PartialView(subjects);
        }
    }
}