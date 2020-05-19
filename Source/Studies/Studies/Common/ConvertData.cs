using System;
using System.Collections.Generic;
using System.Globalization;

namespace WebAdmin.Common
{
    public class ConvertData
    {
        public String parseDate(string date)
        {
            var parsed = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var formatted = parsed.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return formatted;
        }

      
    }
}