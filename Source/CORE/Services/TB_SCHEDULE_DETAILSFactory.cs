using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_SCHEDULE_DETAILSFactory
    {
        public bool Insert(TB_SCHEDULE_DETAILS details)
        {
            return new TB_SCHEDULE_DETAILSSql().Insert(details);
        }
        public bool Update(TB_SCHEDULE_DETAILS details)
        {
            return new TB_SCHEDULE_DETAILSSql().Update(details);
        }
        public bool Delete(TB_SCHEDULE_DETAILS detailsId)
        {
            return new TB_SCHEDULE_DETAILSSql().Delete(detailsId);
        }
        public List<TB_SCHEDULE_DETAILS> GetAll()
        {
            return new TB_SCHEDULE_DETAILSSql().SelectAll();
        }
        public bool CheckRoomClass(string roomId, string timeStart, string timeEnd, string dayOfWeek, string dayOfWeek2, string timeStart2, string timeEnd2)
        {
            bool check = false;
            List<TB_SCHEDULE_DETAILS> list = new TB_SCHEDULE_DETAILSSql().FilterByField("ScheduleDetailRoomClass", roomId);
            if(list.Count ==0)
            {
                check = true;
            }else
            {
                if (string.IsNullOrEmpty(dayOfWeek2))
                {
                    var a = list.Where(x => x.ScheduleDetailDayOfWeek.Equals(dayOfWeek)).ToList();
                    if (a.Count == 0)
                    {
                        check = true;
                    }
                    else
                    {
                        var b = a.Where(x => x.ScheduleDetailTimeFrom.Hours == Int32.Parse(timeStart)).ToList();
                        if (b.Count == 0)
                        {
                            check = true;
                        }
                    }
                }else
                {
                    var a = list.Where(x => x.ScheduleDetailDayOfWeek.Equals(dayOfWeek)).ToList();
                    var a1 = list.Where(x => x.ScheduleDetailDayOfWeek.Equals(dayOfWeek2)).ToList();
                    if (a.Count == 0 && a1.Count ==0)
                    {
                        check = true;
                    }
                    else
                    {
                        var b = a.Where(x => x.ScheduleDetailTimeFrom.Hours == Int32.Parse(timeStart)).ToList();
                        var b1 = a.Where(x => x.ScheduleDetailTimeFrom.Hours == Int32.Parse(timeStart2)).ToList();
                        if (b.Count == 0 && b.Count ==0)
                        {
                            check = true;
                        }
                    }
                }
                
            }
            return check;
        }
    }
}
