using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{

    internal class TB_SCHEDULE_DETAILSSql : DataAccessTable<TB_SCHEDULE_DETAILS>
    {
        public TB_SCHEDULE_DETAILSSql() : base("Studies.ConnectionString")
        {
        }
    }
}
