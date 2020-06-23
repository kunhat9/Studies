using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{

    internal class V_ReportChartSql : DataAccessTable<V_ReportChart>
    {
        public V_ReportChartSql() : base("Studies.ConnectionString")
        {
        }
    }
}
