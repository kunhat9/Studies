using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CORE.Helpers;
using CORE.Tables;
using CORE.Views;
using WebAdmin.Controllers;
using WebAdmin.Models;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class AjaxController : BaseController
    {
        public JsonResult InsertOrUpdateUser(TB_USERS value,bool isUpdate)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                value.UserDateCreated = DateTime.Now;
                value.UserStatus = value.UserStatus.Equals("1") ? "A" : "D";
                bool check = false;
                if (isUpdate)
                {
                    check = User_Service.Update(value);
                }
                else
                {
                    value.UserDateCreated = DateTime.Now;
                    check = User_Service.Insert(value);
                }
                
                
                if (check)
                {
                    Result.Code = 0;
                    Result.Result = "Thành công";
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "Thao tác không thành công";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 1;
                Result.Result = "Thao tác không thành công";
                IOHelper.WriteLog(StartUpPath, IpAddress, "InsertOrUpdateUser:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }
        
        public JsonResult DeleteUser(int id)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                bool check = User_Service.Delete(id);
                

                if (check)
                {
                    Result.Code = 0;
                    Result.Result = "Thành công";
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "Thao tác không thành công";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 1;
                Result.Result = "Thao tác không thành công";
                IOHelper.WriteLog(StartUpPath, IpAddress, "InsertOrUpdateUser:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }
        
        public JsonResult InsertOrUpdateSubjectBox(List<string> listSubjectId,int box_id=0, string boxSubjectId="")
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                bool check = Subjects_Boxes_Service.InsertOrUpdate(box_id,listSubjectId, boxSubjectId);
                
                
                if (check)
                {
                    Result.Code = 0;
                    Result.Result = "Thành công";
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "Thao tác không thành công";
                }
                
            }
            catch (Exception Ex)
            {
                Result.Code = 1;
                Result.Result = "Thao tác không thành công";
                IOHelper.WriteLog(StartUpPath, IpAddress, "InsertOrUpdateUser:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }
        
        public JsonResult DeleteSubjectBox(int id)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                bool check = User_Service.Delete(id);
                

                if (check)
                {
                    Result.Code = 0;
                    Result.Result = "Thành công";
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "Thao tác không thành công";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 1;
                Result.Result = "Thao tác không thành công";
                IOHelper.WriteLog(StartUpPath, IpAddress, "InsertOrUpdateUser:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }
        
        public JsonResult InsertOrUpdateClass(ClassModel value,bool isUpdate)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                //value.Status = value.Status.Equals("active") ? "A" : "D";

                var scheduleId = value.ScheduleId.Equals(0) ? "" : value.ScheduleId.ToString();
                
                bool check = Classes_Service.InsertOrUpdateClassFromAdmin(scheduleId,value.BoxSubjectId.ToString(),value.Price,value.DateStart,value.DateEnd,value.DayOfWeek,value.TimeStart,value.TimeEnd,value.Status,value.UserId,value.UserNote);


                if (check)
                {
                    Result.Code = 0;
                    Result.Result = "Thành công";
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "Thao tác không thành công";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 1;
                Result.Result = "Thao tác không thành công";
                IOHelper.WriteLog(StartUpPath, IpAddress, "InsertOrUpdateUser:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }
        
        public JsonResult DeleteClass(int id)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                bool check = Classes_Service.Delete(id);
                

                if (check)
                {
                    Result.Code = 0;
                    Result.Result = "Thành công";
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "Thao tác không thành công";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 1;
                Result.Result = "Thao tác không thành công";
                IOHelper.WriteLog(StartUpPath, IpAddress, "InsertOrUpdateUser:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }
        public JsonResult AddStudents(int ScheduleId,List<string> Userids, string type)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                //bool check = true;
                
                if (Classes_Service.InsertStudiesToClass(ScheduleId,Userids,type))
                {
                    Result.Code = 0;
                    Result.Result = "Thành công";
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "Thao tác không thành công";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 1;
                Result.Result = "Thao tác không thành công";
                IOHelper.WriteLog(StartUpPath, IpAddress, "InsertOrUpdateUser:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }
        public JsonResult AddTrackingSchedules(string dateTracking, string note, List<string> listUserId, string schedulesId, string type)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                //bool check = true;

                if (Trackings_Service.AddTrackingSchedules(dateTracking,note,listUserId,schedulesId,type))
                {
                    Result.Code = 0;
                    Result.Result = "Thành công";
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "Thao tác không thành công";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 1;
                Result.Result = "Thao tác không thành công";
                IOHelper.WriteLog(StartUpPath, IpAddress, "InsertOrUpdateUser:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }
        public JsonResult GetTeacherByDateWeekTime(string dayOfWeek, string timeFrom , string timeTo)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                List<TB_USERS> listTeacher = new List<TB_USERS>();
                listTeacher = User_Service.GetTeacherByDateWeekTime(dayOfWeek, timeFrom, timeTo);
                Result.Code = 0;
                Result.Result = listTeacher;
              
            }
            catch (Exception Ex)
            {
                Result.Code = 1;
                Result.Result = new List<TB_USERS>();
                IOHelper.WriteLog(StartUpPath, IpAddress, "InsertOrUpdateUser:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }

    }
}