using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Views;
using WebAdmin.Controllers;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class ReportController : BaseController
    {
        // GET: Admin/Report
        // bao cao thong ke 
        public ActionResult Index()
        {
            
            return View();
        }
        
        public PartialViewResult _Index(string start="",string end="",string type="")
        {
            var report = new List<V_ReportChart>();
            try
            {
                report = ReportService.GetReportRevenueChart(start,end,type);
            }
            catch (Exception ex)
            {
                CORE.Helpers.IOHelper.WriteLog(StartUpPath, "ReportController :", ex.Message, ex.ToString());

            } 
            return PartialView(report);
        }
    }
}