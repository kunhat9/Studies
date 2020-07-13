using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Helpers
{
    public static class DateTimeHelper
    {
        public static List<DateTime> DaysOfMonth(int year, int month, DayOfWeek day)
        {
            List<DateTime> dates = new List<DateTime>();
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                DateTime dt = new DateTime(year, month, i);
                if (dt.DayOfWeek == day)
                {
                    dates.Add(dt);
                }
            }
            return dates;
        }
    }
}
