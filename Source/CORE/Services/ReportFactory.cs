using CORE.Internal.ViewSql;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class ReportFactory
    {
        public HomePage GetHomePage()
        {
            return new HomePageSql().SelectFromStore(AppSettingKeys.GET_HOME_PAGE).FirstOrDefault();
        }
        public List<V_ReportChart> GetReportRevenueChart(string startDate, string endDate, string type)
        {
            // type = DAY,MONTH, YEAR
            return new V_ReportChartSql().SelectFromStore(AppSettingKeys.REPORT_REVENUE_CHART, startDate, endDate, type.ToUpper());
        }
    }
}
