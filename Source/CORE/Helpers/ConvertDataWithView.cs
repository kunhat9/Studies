using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Helpers
{
    public static class ConvertDataWithView
    {
        public static string Date_yyyyMMdd_ToView(this string date, bool now = false)
        {
            try
            {
                return string.IsNullOrEmpty(date) ? now ? DateTime.Now.ToString("dd/MM/yyyy") : "" : DateTime.ParseExact(date, "yyyyMMdd", null).ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Currency_ToView(this decimal currency, bool def = false)
        {
            try
            {
                return currency == 0 ? def ? "0" : "" : string.Format("{0:N0}", currency);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string Convert_DayOfweek(string key)
        {
            string result = "";
            switch (key)
            {
                case "THU2": result = "Thứ 2"; break;
                case "THU3": result = "Thứ 3"; break;
                case "THU4": result = "Thứ 4"; break;
                case "THU5": result = "Thứ 5"; break;
                case "THU6": result = "Thứ 6"; break;
                case "THU7": result = "Thứ 7"; break;
                case "CN": result = "Chủ nhật"; break;
            }
            return result;
        }
        public static string Int_ToView(this int i)
        {
            try
            {
                return i == 0 ? "" : "" + i;
            }
            catch (Exception)
            {
                return "";
            }
        }
        
    }
}
