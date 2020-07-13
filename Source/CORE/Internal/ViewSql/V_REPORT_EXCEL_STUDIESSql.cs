using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_REPORT_EXCEL_STUDIESSql : DataAccessTable<V_REPORT_EXCEL_STUDIES>
    {
        public V_REPORT_EXCEL_STUDIESSql() : base("Studies.ConnectionString")
        {
        }
    }

}
