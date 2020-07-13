using CORE.Internal;
using CORE.Tables;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_TRACKINGSFactory
    {
        public bool Insert(TB_TRACKINGS tracking)
        {
            return new TB_TRACKINGSSql().Insert(tracking);
        }
        public bool Update(TB_TRACKINGS tracking)
        {
            return new TB_TRACKINGSSql().Update(tracking);
        }
        public bool Delete(int trackingId)
        {
            return new TB_TRACKINGSSql().Delete(trackingId);
        }
        public TB_TRACKINGS GetById(int trackingId)
        {
            return new TB_TRACKINGSSql().SelectByPrimaryKey(trackingId);
        }

        public List<TB_TRACKINGS> GetAll()
        {
            return new TB_TRACKINGSSql().SelectAll();
        }
        // them danh sach diem danh theo lop
        public bool AddTrackingSchedules(string dateTracking, string note, List<string> listUserId, string schedulesId, string type)
        {
            string edesc, ecode;
            TB_TRACKINGSSql sql = new TB_TRACKINGSSql();
            string listId = String.Join(",", listUserId);
            sql.SelectFromStore(out ecode, out edesc, AppSettingKeys.ADD_TRACKING_BY_SCHEDULES, dateTracking, note, listId, schedulesId, type);
            if (ecode.Equals("00"))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        // danh sach diem danh cua hoc sinh theo lop
        public List<V_TRACKING_USER_CLASS> GetTrackingUser(string userId, string scheduleId, string startDate, string endDate)
        {
            List<TB_TRACKINGS> listTracking = new List<TB_TRACKINGS>();
            listTracking = GetAll();
            List<V_TRACKING_USER_CLASS> list = new List<V_TRACKING_USER_CLASS>();
            var result = listTracking.GroupBy(x => new
            {
                userId = x.TrackingUserId,
                schedueId = x.TrackingScheduleId
            }).Select(y => new
            {
                userId = y.Key.userId,
                schedueId = y.Key.schedueId,
                dateTracking = listTracking.
                Where(t => t.TrackingUserId == y.Key.userId && t.TrackingScheduleId == y.Key.schedueId)
                .Select(g => g.TrackingDate)
                .Where(h => (!string.IsNullOrEmpty(startDate) ? Int32.Parse(h.ToString("yyyyMMdd")) >= Int32.Parse(startDate) : true)
                && (!string.IsNullOrEmpty(startDate) ? Int32.Parse(h.ToString("yyyyMMdd")) <= Int32.Parse(endDate) : true))
                .ToList()
            }).Where(j => (!string.IsNullOrEmpty(userId) ? j.userId == Int32.Parse(userId) : true)
            && (!string.IsNullOrEmpty(scheduleId) ? j.schedueId == Int32.Parse(scheduleId) : true)
            ).ToList();
            foreach (var item in result)
            {
                V_TRACKING_USER_CLASS v = new V_TRACKING_USER_CLASS();
                v.userId = item.userId;
                v.schedueId = item.schedueId;
                v.dateTracking = item.dateTracking;
                list.Add(v);
            }

            return list;
        }

        public List<V_TRACKING_SCHEDULE> GetTotalTrackingBy(string scheduleId, string startDate, string endDate)
        {
            List<TB_TRACKINGS> listTracking = new List<TB_TRACKINGS>();
            listTracking = GetAll();
            List<V_TRACKING_SCHEDULE> list = new List<V_TRACKING_SCHEDULE>();
            var result = listTracking.GroupBy(x => new
            {
                ScheduleId = x.TrackingScheduleId
            }).Select(y => new
            {
                ScheduleId = y.Key.ScheduleId,
                Details = listTracking.GroupBy(j=>new {
                    dateTracking = j.TrackingDate
                }).Select(t=> new {
                    dateTracking = t.Key.dateTracking,
                    total = y.Where(h => (!string.IsNullOrEmpty(startDate) ? h.TrackingDate.ToString(AppSettingKeys.FOMAT_DATE_INPUT).Equals(startDate) : true)
                && (!string.IsNullOrEmpty(startDate) ? h.TrackingDate.ToString(AppSettingKeys.FOMAT_DATE_INPUT).Equals(endDate) : true) 
                && h.TrackingDate.ToString("dd/MM/yyyy").Equals(t.Key.dateTracking.ToString("dd/MM/yyyy"))).Count()
                    //.Where(k=>k.TrackingDate.ToString("dd/MM/yyyy").Equals(t.TrackingDate.ToString("dd/MM/yyyy"))).Count()
                }).ToList()
            }).Where(x=>(!string.IsNullOrEmpty(scheduleId)) ?x.ScheduleId.ToString().Equals(scheduleId):true).ToList();
            foreach (var item in result)
            {
                V_TRACKING_SCHEDULE v = new V_TRACKING_SCHEDULE();
                v.schedulesId = item.ScheduleId;
                List<V_TRACKING_DETAILS> temp = new List<V_TRACKING_DETAILS>();
                foreach(var d in item.Details)
                {
                    V_TRACKING_DETAILS c = new V_TRACKING_DETAILS();
                    c.dateTracking = d.dateTracking;
                    c.TotalTracking = d.total;
                    temp.Add(c);
                }
                v.details = temp;
                list.Add(v);
            }
            return list;
        }
    }
}
