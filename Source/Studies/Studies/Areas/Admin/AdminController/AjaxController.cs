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
                value.UserStatus = value.UserStatus.Equals("1") ? "A" : "D";
                bool check = false;
                if (isUpdate)
                {
                    check = User_Service.Update(value);
                }
                else
                {
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
        
        public JsonResult InsertOrUpdateSubjectBox(List<string> listSubjectId,int box_id=0)
        {
            AjaxResultModel Result = new AjaxResultModel();
            try
            {
                bool check = Subjects_Boxes_Service.InsertOrUpdate(box_id,listSubjectId);
                
                
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
    }
}