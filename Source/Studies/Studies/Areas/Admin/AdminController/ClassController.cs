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
    public class ClassController : BaseController
    {
        // GET: Admin/Class
        // quan ly lop hoc 
        public ActionResult Index()
        {
            return View();
        }
        
        public PartialViewResult _Index(string keyText = "",string status = "", int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.maxNumber = 0;
            var classes = new List<V_CLASS>();

            try
            {
                int count = 0;
                classes = Classes_Service.GetClassBy(keyText,"","","","",status, pageNumber, pageSize, out count);
                ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);

                ViewBag.boxSubjects = Subjects_Boxes_Service.GetAll();
            }
            catch (Exception ex)
            {
                CORE.Helpers.IOHelper.WriteLog(StartUpPath, "ClassController :", ex.Message, ex.ToString());
            }

            return PartialView(classes);
        }
        
        public PartialViewResult _Detail()
        {
            List<TB_SUBJECTS> subjects = new List<TB_SUBJECTS>();
            List<TB_BOXES> boxes = new List<TB_BOXES>();
            List<TB_USERS> users = new List<TB_USERS>();
            List<TB_USERS> teachers = new List<TB_USERS>();


            try
            {
                subjects = Subjects_Service.GetAll();
                boxes = Boxes_Service.GetAll();
                users = User_Service.GetAll();
                foreach (var user in users)
                {
                    if (user.UserType.Equals("TEACHER"))
                    {
                        teachers.Add(user);
                    }
                }
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            ViewBag.subjects = subjects;
            ViewBag.boxes = boxes;
            ViewBag.teachers = teachers;

            return PartialView();
        }
        
        public ActionResult Class_Detail(int id)
        {
            List<TB_SUBJECTS> subjects = new List<TB_SUBJECTS>();
            List<TB_BOXES> boxes = new List<TB_BOXES>();
            List<TB_USERS> users = new List<TB_USERS>();
            List<TB_USERS> teachers = new List<TB_USERS>();
            List<TB_USERS> studies = new List<TB_USERS>();
            List<TB_USERS> students = new List<TB_USERS>();

            var classes = new TB_CLASSES();
            var details = new List<V_CLASS>();
            try
            {
                subjects = Subjects_Service.GetAll();
                boxes = Boxes_Service.GetAll();
                users = User_Service.GetAll();
                int count = 0;
                details = Classes_Service.GetClassBy("","","","","","", 1, Int16.MaxValue, out count);

                studies = User_Service.GetStudiesBySchedule(id, 1, Int16.MaxValue, out count);


                foreach (var user in users)
                {
                    if (user.UserType.Equals("TEACHER"))
                    {
                        teachers.Add(user);
                    }
                }
                
                foreach (var user in users)
                {
                    if (user.UserType.Equals("STUDIES"))
                    {
                        students.Add(user);
                    }
                }
                
                classes = Classes_Service.GetById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            ViewBag.subjects = subjects;
            ViewBag.boxes = boxes;
            ViewBag.teachers = teachers;
            ViewBag.details = details;
            ViewBag.studies = studies;
            ViewBag.students = students;
            ViewBag.id = id;


            return PartialView(classes);
        }
    }
}