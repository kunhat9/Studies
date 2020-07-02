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
                .Where(h => (!string.IsNullOrEmpty(startDate) ? h.ToString(AppSettingKeys.FOMAT_DATE_INPUT).Equals(startDate) : true)
                && (!string.IsNullOrEmpty(startDate) ? h.ToString(AppSettingKeys.FOMAT_DATE_INPUT).Equals(endDate) : true))
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
    }
}
