using CORE.Internal;
using CORE.Internal.ViewSql;
using CORE.Tables;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_POINTSFactory
    {
        public bool Insert(TB_POINTS point)
        {
            return new TB_POINTSSql().Insert(point);
        }
        public bool Update(TB_POINTS point)
        {
            return new TB_POINTSSql().Update(point);
        }
        public bool Delete(int pointId)
        {
            return new TB_POINTSSql().Delete(pointId);
        }
        public List<TB_POINTS> GetAll()
        {
            return new TB_POINTSSql().SelectAll();
        }
        public TB_POINTS GetById(int pointId)
        {
            return new TB_POINTSSql().SelectByPrimaryKey(pointId);
        }
        // PointType = DAUKI , GIUAKI, CUOIKI
        // bang diem theoo lop , bang diem cua hoc sinh 
        public List<V_TransciptStudies> GetTranscriptBy(string userId, string scheduleId, string type, int pageNumber, int pageSize, out int count)
        {
            object cTemp;
            List<V_TransciptStudies> list = new List<V_TransciptStudies>();
            list = new V_TransciptStudiesSql().SelectFromStoreOutParam(AppSettingKeys.GET_TRANSCIPT_BY, out cTemp, userId, scheduleId, type, pageNumber, pageSize);
            count = (int)cTemp;
            return list;
        }
        public V_ResultCallSQL InsertPointStudiesFromClass(string schedulesId, List<V_Point> listPoint)
        {
            V_ResultCallSQL result = new V_ResultCallSQL();
            string xml = "";
            foreach (var item in listPoint)
            {
                xml += "<row>" + item.ToStringXml() + "</row>";
            }
            string xmlResult = "<row>" + xml + "</row>";
            string ecode, edesc;
            TB_POINTSSql sql = new TB_POINTSSql();
            sql.SelectFromStore(out ecode, out edesc, AppSettingKeys.INSERT_POINT_TO_CLASS, schedulesId, xmlResult);
            result.ecode = ecode;
            result.edesc = edesc;
            return result;
        }
        // them diem cho hoc sinh theo lop
    }
}
