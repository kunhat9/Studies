using CORE.Services;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using CORE.Tables;
using WebAdmin.Configuration;

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
    }
}