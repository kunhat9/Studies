using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using WebAdmin.Configuration;

namespace WebAdmin.Controllers
{
    [AuthorizeBusiness]
    public class BaseController : Controller
    {
    


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
    }
}