using CORE.Services;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using CORE.Views;
using CORE.Helpers;
using WebAdmin.Configuration;

using Microsoft.Office.Interop.Excel;
using CORE.Helpers;
using System.Collections.Generic;
using CORE.Tables;
using WebAdmin.AppSession;

namespace WebAdmin.Controllers
{
    [AuthorizeBusiness]
    public class BaseController : Controller
    {
        protected TB_FILESFactory Files_Service = new TB_FILESFactory();
        protected TB_TRACKINGSFactory Tracking_Serivce = new TB_TRACKINGSFactory();
        protected TB_USERSFactory User_Service = new TB_USERSFactory();
        protected TB_TEACHING_SCHEDULESFactory TeachingSchedules_Service = new TB_TEACHING_SCHEDULESFactory();
        protected TB_REGISTERSFactory Registers_Service = new TB_REGISTERSFactory();
        protected TB_BOXESFactory Boxes_Service = new TB_BOXESFactory();
        protected TB_POINTSFactory Points_Service = new TB_POINTSFactory();
        protected TB_CLASSESFactory Classes_Service = new TB_CLASSESFactory();
        protected TB_SCHEDULESFactory Schedules_Service = new TB_SCHEDULESFactory();
        protected TB_TRACKINGSFactory Trackings_Service = new TB_TRACKINGSFactory();
        protected TB_SUBJECTSFactory Subjects_Service = new TB_SUBJECTSFactory();
        protected TB_BOX_SUBJECTSFactory Subjects_Boxes_Service = new TB_BOX_SUBJECTSFactory();
        protected ReportFactory ReportService = new ReportFactory();
        protected TB_SCHEDULE_DETAILSFactory Schedule_Detail_Service = new TB_SCHEDULE_DETAILSFactory();
        protected TB_ROOM_CLASSFactory RoomClass_Service = new TB_ROOM_CLASSFactory();
        protected TB_TRANSACTIONFactory Transaction_Service = new TB_TRANSACTIONFactory();
        protected string GetMD5Hash(string rawString)
        {
            UnicodeEncoding encode = new UnicodeEncoding();
            //Chuyển kiểu chuổi thành kiểu byte
            Byte[] passwordBytes = encode.GetBytes(rawString);
            //mã hóa chuỗi đã chuyển
            Byte[] hash;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            hash = md5.ComputeHash(passwordBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();
            foreach (byte outputByte in hash)
            {
                sb.Append(outputByte.ToString("x2").ToUpper());
                //nếu bạn muốn các chữ cái in thường thay vì in hoa thì bạn thay chữ "X" in hoa 
                //trong "X2" thành "x"
            }
            return sb.ToString();
        }

        protected string GenKey(string hddCode, int day, DateTime now, string boxId)
        {
            try
            {
                string end = now.AddDays(day).ToString("yyyy-MM-dd");
                return (day + "-" + GetMD5Hash(day + hddCode + end + boxId.ToUpper()) + "-" + boxId.ToUpper() + "-" + end).ToUpper();
            }
            catch
            {
                return null;
            }
        }

        protected string GetIP()
        {
            try
            {
                return Request.UserHostAddress;
            }
            catch (Exception) { return ""; }
        }
        protected string StartUpPath
        {
            get
            {
                try
                {
                    return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                }
                catch (Exception)
                {
                    return "/Logs";
                }
            }
        }

        protected string IpAddress
        {
            get
            {
                string visitorIPAddress = "";
                try
                {
                    bool GetLan = false;
                    visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (string.IsNullOrEmpty(visitorIPAddress))
                        visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];

                    if (string.IsNullOrEmpty(visitorIPAddress))
                        visitorIPAddress = Request.UserHostAddress;

                    if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
                    {
                        GetLan = true;
                        visitorIPAddress = string.Empty;
                    }

                    if (GetLan)
                    {
                        if (string.IsNullOrEmpty(visitorIPAddress))
                        {
                            //This is for Local(LAN) Connected ID Address
                            string stringHostName = Dns.GetHostName();
                            //Get Ip Host Entry
                            IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                            //Get Ip Address From The Ip Host Entry Address List
                            IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                            try
                            {
                                visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                            }
                            catch
                            {
                                try
                                {
                                    visitorIPAddress = arrIpAddress[0].ToString();
                                }
                                catch
                                {
                                    try
                                    {
                                        arrIpAddress = Dns.GetHostAddresses(stringHostName);
                                        visitorIPAddress = arrIpAddress[0].ToString();
                                    }
                                    catch
                                    {
                                        visitorIPAddress = string.Empty;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    CORE.Helpers.IOHelper.WriteLog(StartUpPath, "BaseController :", ex.Message, ex.ToString());
                }
                return visitorIPAddress;
            }
        }

        protected string FilePath
        {
            get
            {
                try
                {
                    string folderPath = Server.MapPath("~");
                    return folderPath;
                }
                catch (Exception)
                {
                    return "/Logs";
                }
            }
        }

        protected string BillExport(string filePath, string userId, string startDate, string endDate, string type)
        {
            Application xlApp = new Application();
            if (xlApp == null)
            {
                return "Lỗi không thể sử dụng được thư viện EXCEL";
            }

            try
            {
                if (string.IsNullOrEmpty(startDate))
                {
                    return "Vui lòng chọn ngày bắt đầu";
                }
                else if (string.IsNullOrEmpty(endDate))
                {
                    return "Vui lòng chọn ngày kết thúc";
                }


                List<V_REPORT_EXCEL_STUDIES> data = User_Service.ReportTutionStudies(userId, startDate, endDate);
                if (data == null || data.Count == 0)
                {
                    return "Lỗi không có hóa đơn";
                }

                xlApp.DisplayAlerts = false;
                xlApp.Visible = false;
                object missing = Type.Missing;
                Workbook wb = xlApp.Workbooks.Add(missing);
                Worksheet ws = (Worksheet)wb.Worksheets[1];

                int fontSizeTieuDe = 18;
                int fontSizeTenTruong = 14;
                int fontSizeNoiDung = 12;

                DateTime dateStart = new DateTime();
                DateTime.TryParseExact(startDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dateStart);
                DateTime dateEnd = new DateTime();
                DateTime.TryParseExact(endDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dateEnd);
                //Info
                ws.AddValue("B2", "E2", "Trung tâm bồi dưỡng kiến thức", fontSizeTieuDe, true, XlHAlign.xlHAlignCenter, false, 20);
                ws.AddValue("B3", "E3", "Trung tâm ", fontSizeTenTruong, true, XlHAlign.xlHAlignCenter, false);

                ws.AddValue("H2", "K2", "Cộng hòa Xã hội Chủ nghĩa Việt Nam", fontSizeTieuDe, true, XlHAlign.xlHAlignCenter, false, 20);
                ws.AddValue("H3", "K3", "   Độc lập - Tự do - Hạnh phúc", fontSizeTenTruong, true, XlHAlign.xlHAlignCenter, false);
                ws.AddValue("E5", "G5", "Thông báo", fontSizeTenTruong, true, XlHAlign.xlHAlignCenter);
                ws.AddValue("E6", "G6", "Học phí từ " + dateStart.ToString("dd/MM/yyy") + " đến " + dateEnd.ToString("dd/MM/yyy"), fontSizeTenTruong, true, XlHAlign.xlHAlignCenter, false);

                ws.AddValue("A8", "A8", "Kính gửi phụ huynh học sinh " + data[0].UserFullName, fontSizeTenTruong, false, XlHAlign.xlHAlignLeft, false);
                ws.AddValue("A9", "A9", "Trung tâm bồi dưỡng kiến thức Chu Văn An thông báo thu học phí từ " + dateStart.ToString("dd/MM/yyy") + " đến " + dateEnd.ToString("dd/MM/yyy") + " bao gồm:", fontSizeTenTruong, false, XlHAlign.xlHAlignLeft, false);


                int rowStart = 11, rowIndex = 11;
                //Header
                ws.AddValue("E" + rowIndex, "E" + rowIndex, "Môn học", fontSizeTenTruong, true, XlHAlign.xlHAlignCenter, true, 12);
                ws.AddValue("F" + rowIndex, "F" + rowIndex, "Số buổi", fontSizeTenTruong, true, XlHAlign.xlHAlignCenter, true, 12);
                ws.AddValue("G" + rowIndex, "G" + rowIndex, "Thành tiền", fontSizeTenTruong, true, XlHAlign.xlHAlignCenter, true, 18);
                rowIndex += 1;
                decimal total = 0;



                //Body

                for (int i = 0; i < data.Count; i++)
                {
                    string subjectName = Subjects_Service.GetByBoxScheduleId(data[i].ScheduleId).SubjectName;
                    dynamic[] val = { subjectName, data[i].CountNumber
                            , data[i].TuitionStudies };
                    ws.AddValue("E" + rowIndex, "G" + rowIndex, val, fontSizeNoiDung, false, XlHAlign.xlHAlignLeft, false);
                    total += data[i].TuitionStudies;
                    rowIndex += 1;
                }





                //End
                ws.AddValue("F" + rowIndex, "F" + rowIndex, "TỔNG CỘNG", fontSizeTenTruong, true, XlHAlign.xlHAlignCenter, true, 18);
                ws.AddValue("G" + rowIndex, "G" + rowIndex, total, fontSizeTenTruong);

                //Sign
                string fullName = "";
                if (Session[AppSessionKeys.USER_INFO] != null)
                {
                    fullName = ((TB_USERS)Session[AppSessionKeys.USER_INFO]).UserFullName;
                }
                ws.AddValue("D" + (rowIndex + 2), "G" + (rowIndex + 2), "NHÂN VIÊN", fontSizeTenTruong, true);
                ws.AddValue("D" + (rowIndex + 8), "G" + (rowIndex + 8), fullName, fontSizeTenTruong, true);

                //Border
                ws.get_Range("E" + rowStart, "G" + rowIndex).SetBorderAround();

                //Save
                wb.SaveAs(filePath);
                wb.Close(true, missing, missing);
                //wb.SaveAs(filePath, XlFileFormat.xlOpenXMLWorkbook, missing, missing, false, false
                //    , XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlUserResolution
                //    , true, missing, missing, missing);
                //wb.Saved = true;
                //wb.Close();

                //thoát và thu hồi bộ nhớ cho COM
                ws.ReleaseObject();
                wb.ReleaseObject();

                return "";
            }
            catch (Exception ex)
            {
                //return ex.ToString();
                throw ex;
            }
            finally
            {
                if (xlApp != null)
                {
                    xlApp.Quit();
                }
                xlApp.ReleaseObject();
            }
        }
    }
}