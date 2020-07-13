using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_REPORT_TUITIONSql : DataAccessTable<V_REPORT_TUITION>
    {
        public V_REPORT_TUITIONSql() : base("Studies.ConnectionString")
        {
        }
    }
}
