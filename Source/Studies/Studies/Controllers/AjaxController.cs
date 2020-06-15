using CORE.Helpers;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Configuration;
using WebAdmin.Models;

namespace WebAdmin.Controllers
{
    public class AjaxController : BaseController
    {
        #region Upload File
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
                Result.Code = 1;
                Result.Result = "Thao tác không thành công";
                IOHelper.WriteLog(StartUpPath, IpAddress, "UploadFile:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }

        #endregion
        

        public JsonResult TeachingSchedules(List<TB_TEACHING_SCHEDULES> schedules,int userid)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                TB_USERS user = User_Service.GetById(userid);

                int quantity = TeachingSchedules_Service.InsertTeacherAndSchedule(user, schedules);

                if (quantity > 0)
                {
                    Result.Code = 0;
                    Result.Result = "Đăng kí lịch học thành công";
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
                IOHelper.WriteLog(StartUpPath, IpAddress, "TeachingSchedules:", Ex.Message, Ex.ToString());
            }
            return Json(new JsonResult() { Data = Result });
        }
        
        public JsonResult Enrollment(TB_REGISTERS register)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                bool check = Registers_Service.Insert(register);

                if (check)
                {
                    Result.Code = 0;
                    Result.Result = "Đăng kí lịch học thành công";
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
                IOHelper.WriteLog(StartUpPath, IpAddress, "Product::_ChiTiet:GetAllProvider:", Ex.Message, Ex.ToString());
                throw;
            }
            return Json(new JsonResult() { Data = Result });
        }
    }

}