﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CORE.Tables;
using CORE.Views;
using WebAdmin.Controllers;
using WebAdmin.Configuration;

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
        [AjaxChildActionOnly]
        public PartialViewResult _Index(string keyText = "", string boxId = "", string subjectId = "", string timeIn = "", string timeEnd = "", string status = "", int pageNumber = 1, int pageSize = 10)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.maxNumber = 0;
            List<V_CLASS_DETAILS> cl = new List<V_CLASS_DETAILS>();
            var classes = new List<V_CLASS>();
            List<V_NUMBER_STUDIES> listCount = new List<V_NUMBER_STUDIES>();
            int count = 0;
            try
            {
                cl = Classes_Service.GetInfoClass(keyText, boxId, subjectId, timeIn, timeEnd, status, pageNumber, pageSize, out count);
                listCount = Schedules_Service.GetCountStudieInClass("");
                //classes = Classes_Service.GetClassBy(keyText, boxId, subjectId, timeIn, timeEnd, status, pageNumber, pageSize, out count);
                ViewBag.maxNumber = Math.Ceiling((double)count / pageSize);
                ViewBag.users = User_Service.GetAll();
                int count1 = 0;
                ViewBag.boxSubjects = Subjects_Boxes_Service.GetAllBy("", 1, short.MaxValue, out count1);
            }
            catch (Exception ex)
            {
                CORE.Helpers.IOHelper.WriteLog(StartUpPath, "ClassController :", ex.Message, ex.ToString());
            }
            ViewBag.Count = listCount;
            return PartialView(cl);
        }

        public PartialViewResult _Detail()
        {
            List<TB_SUBJECTS> subjects = new List<TB_SUBJECTS>();
            List<TB_BOXES> boxes = new List<TB_BOXES>();
       
            List<TB_USERS> teachers = new List<TB_USERS>();
            List<V_BOX_SUBJECT> boxSubject = new List<V_BOX_SUBJECT>();
            List<TB_ROOM_CLASS> room = new List<TB_ROOM_CLASS>();
            int count = 0;
            try
            {
                room = RoomClass_Service.GetAll();
                boxSubject = Subjects_Boxes_Service.GetAllBy("", 1, short.MaxValue, out count);
                subjects = Subjects_Service.GetAll();
                boxes = Boxes_Service.GetAll();
                teachers = User_Service.GetAllTeacher();
               


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            ViewBag.BoxSubject = boxSubject;
            ViewBag.subjects = subjects;
            ViewBag.boxes = boxes;
            ViewBag.teachers = teachers;
            ViewBag.Room = room;
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
            List<TB_REGISTERS> register = new List<TB_REGISTERS>();
            var detail = new V_CLASS();
            var classes = new TB_SCHEDULES();
            var details = new List<V_CLASS>();
            try
            {

                subjects = Subjects_Service.GetAll();
                boxes = Boxes_Service.GetAll();
                users = User_Service.GetUserRegister(id.ToString());
                int count = 0;
                details = Classes_Service.GetClassBy("", "", "", "", "", "", 1, Int16.MaxValue, out count);
                register = Registers_Service.GetAll();
                studies = User_Service.GetStudiesBySchedule(id.ToString(), 1, Int16.MaxValue, out count);
                foreach (var tmp in details)
                {
                    if (id.Equals(tmp.ScheduleId))
                    {
                        detail = tmp;
                    }
                }

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

                classes = Schedules_Service.GetById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            List<TB_USERS> lst = new List<TB_USERS>();
            foreach (var item in students)
            {

                if (studies.Where(x => x.UserId == item.UserId).ToList().Count == 0)
                {
                    lst.Add(item);
                }
            }
            ViewBag.Register = register;
            ViewBag.subjects = subjects;
            ViewBag.boxes = boxes;
            ViewBag.teachers = teachers;
            ViewBag.detail = detail;
            ViewBag.studies = studies;
            ViewBag.students = lst;
            ViewBag.id = id;



            return PartialView(classes);
        }
    }
}