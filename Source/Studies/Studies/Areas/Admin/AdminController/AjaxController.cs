using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CORE.Helpers;
using CORE.Tables;
using CORE.Views;
using WebAdmin.Controllers;
using WebAdmin.Models;
using System.Web;
using System.IO;

namespace WebAdmin.Areas.Admin.AdminController
{
    public class AjaxController : BaseController
    {

        public JsonResult UploadFiles(List<HttpPostedFileBase> files)
        {
            string folderPath = Server.MapPath("~/Files");
            string filePath = "";
            AjaxResultModel Result = new AjaxResultModel();
            List<TB_FILES> data = new List<TB_FILES>();
            try
            {
                if (!folderPath.EndsWith("/"))
                    filePath += "/" + DateTime.Now.ToString("yyyy_MM_dd") + "/";
                else
                    filePath += DateTime.Now.ToString("yyyy_MM_dd") + "/";
                IOHelper.CreateFolder(folderPath + filePath);
                int value = -1;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var InputFileName = DateTime.Now.ToString("yyyyMMddHHmmss_") + Path.GetFileName(file.FileName);
                        //var ServerSavePath = Path.Combine(Server.MapPath("~/Files/") + InputFileName);
                        TB_FILES fTemp = new TB_FILES();
                        fTemp.FileName = Path.GetFileName(file.FileName);
                        fTemp.FileUrl = "/Files" + filePath + InputFileName;
                        fTemp.FileRef = InputFileName;
                        fTemp.FileType = file.ContentType;
                        fTemp.FileService = "USER";
                        file.SaveAs(folderPath + filePath + InputFileName);
                        value = Files_Service.AddFile(fTemp);
                        if (value != -1)
                        {
                            data.Add(fTemp);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (value != -1)
                    {
                        Result.Code = 0;
                        Result.Result = value;
                    }
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "Thao tác không thành công";
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 0;
                IOHelper.WriteLog(StartUpPath, IpAddress, "Product::_ChiTiet:GetAllProvider:", Ex.Message, Ex.ToString());
                throw;
            }
            return Json(new JsonResult() { Data = Result });
        }

        public JsonResult ExportProduct(string userId, string startDate, string endDate, string type )
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                TB_USERS user = new TB_USERS();
                user = User_Service.GetById(Int32.Parse(userId));
                string fileName = "\\Export\\HD" + user.UserFullName + "_" + startDate + "_" + endDate + ".xls";
                string filePath = HttpContext.Server.MapPath("~" + fileName);
                string err = "";
                if (type.Equals("STUDIES"))
                {
                    err = BillExport(filePath, userId, startDate, endDate, type);
                }else
                {
                    err = User_Service.ReportSalaryTeacher(userId, startDate, endDate);
                    if (err.Equals("00"))
                    {
                        err = "";
                    }
                }

                if (string.IsNullOrEmpty(err))
                {
                    Result.Code = 0;
                    Result.Result = fileName;
                }
                else
                {
                    Result.Code = 999;
                    Result.Result = err;
                }
            }
            catch (Exception Ex)
            {
                Result.Code = 2000;
                Result.Result = "Có lỗi xảy ra. Vui lòng thử lại sau hoặc liên hệ với người quản trị.";
                IOHelper.WriteLog(StartUpPath, IpAddress, "Ajax::ExportProduct :", Ex.Message, Ex.ToString());
            }
            return Json(Result);
        }
        public JsonResult UpdateStatusUser(TB_USERS value)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                value.UserStatus = "A";
                value.UserDateCreated = DateTime.Now;
                bool check = false;
                check = User_Service.Update(value);
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
        public JsonResult InsertOrUpdateUser(TB_USERS value, bool isUpdate)
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

        public JsonResult InsertOrUpdateSubjectBox(List<string> listSubjectId, int box_id = 0, string boxSubjectId = "", string price = "")
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                bool check = Subjects_Boxes_Service.InsertOrUpdate(box_id, listSubjectId, boxSubjectId, price);


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


        public JsonResult InsertOrUpdateClass(ClassModel value, bool isUpdate)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                //value.Status = value.Status.Equals("active") ? "A" : "D";

                var scheduleId = value.ScheduleId.Equals(0) ? "" : value.ScheduleId.ToString();

                bool check = Classes_Service.InsertOrUpdateClassFromAdmin(scheduleId, value.BoxSubjectId.ToString(), value.Price, value.DateStart, value.DateEnd, value.DayOfWeek, value.TimeStart, value.TimeEnd, value.Status, value.UserId, value.UserNote, value.ScheduleFileId,value.RoomId);

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
        public JsonResult AddStudents(int ScheduleId, List<string> Userids, string type)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                //bool check = true;
                string check = Classes_Service.InsertStudiesToClass(ScheduleId, Userids, type);
                if (check.Equals("00"))
                {
                    Result.Code = 0;
                    Result.Result = "Thành công";
                }
                else if (check.Equals("150"))
                {
                    Result.Code = 1;
                    Result.Result = "Lớp học đã đủ số lượng thành viên";
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

                if (Trackings_Service.AddTrackingSchedules(dateTracking, note, listUserId, schedulesId, type))
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
        public JsonResult GetTeacherByDateWeekTime(string dayOfWeek, string timeFrom, string timeTo)
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

        public JsonResult CheckRoomClass(string roomId,string dayOfWeek, string timeFrom, string timeTo)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                if (Schedule_Detail_Service.CheckRoomClass(roomId, timeFrom, timeTo, dayOfWeek))
                {
                    Result.Code = 0;
                    Result.Result = roomId;
                }
                else
                {
                    Result.Code = 1;
                    Result.Result = "";
                }

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