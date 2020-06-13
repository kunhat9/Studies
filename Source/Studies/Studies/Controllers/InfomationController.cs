using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Tables;
using CORE.Views;
using WebAdmin.AppSession;
using WebAdmin.Models;

namespace WebAdmin.Controllers
{
    public class InfomationController : BaseController
    {
        // GET: Infomation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Schedules()
        {
            List<TB_SCHEDULES> list = new List<TB_SCHEDULES>();

            try
            {
                list = Schedules_Service.GetAll();
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View(list);
        }
        
        public ActionResult RollCall()
        {
            List<V_TRACKING_USER_CLASS> list = new List<V_TRACKING_USER_CLASS>();

            try
            {
                var user = (TB_USERS)Session[AppSessionKeys.USER_INFO];

                list = Trackings_Service.GetTrackingUser(user.UserId.ToString(),null,null,null);
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View(list);
        }
        
        public ActionResult Tuition()
        {
            List<TB_SCHEDULES> list = new List<TB_SCHEDULES>();

            try
            {
                list = Schedules_Service.GetAll();
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View(list);
        }
        
        public ActionResult Score()
        {
            List<TB_POINTS> list = new List<TB_POINTS>();
            List<ScoreModel> points = new List<ScoreModel>();
            try
            {
                list = Points_Service.GetAll();

                foreach (var tmp in list)
                {
                    bool check = false;
                    foreach (var point in points)
                    {

                        if (point.PointClassId.Equals(tmp.PointClassId))
                        {

                            check = true;
                            point.PointNumber.Add(tmp.PointNumber);

                        }
                        
                    }

                    if (!check)
                    {
                        points.Add(new ScoreModel(){PointClassId = tmp.PointClassId,PointNumber = new List<decimal>(){tmp.PointNumber}});
                    }
                    
                }
                
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View(points);
        }
    }
}