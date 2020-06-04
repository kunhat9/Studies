using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_SCHEDULE_DETAILSSql : DataAccessTable<V_SCHEDULE_DETAILS>
    {
        public V_SCHEDULE_DETAILSSql() : base("Studies.ConnectionString")
        {
        }
    }
}
