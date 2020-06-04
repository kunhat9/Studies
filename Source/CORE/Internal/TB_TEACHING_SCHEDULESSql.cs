using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
    internal class TB_TEACHING_SCHEDULESSql : DataAccessTable<TB_TEACHING_SCHEDULES>
    {
        public TB_TEACHING_SCHEDULESSql() : base("Studies.ConnectionString")
        {
        }
    }
}
