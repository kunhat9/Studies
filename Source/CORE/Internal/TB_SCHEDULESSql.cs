using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
   
    internal class TB_SCHEDULESSql : DataAccessTable<TB_SCHEDULES>
    {
        public TB_SCHEDULESSql() : base("Studies.ConnectionString")
        {
        }
    }
}
