using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
    internal class TB_TRACKINGSSql : DataAccessTable<TB_TRACKINGS>
    {
        public TB_TRACKINGSSql() : base("Studies.ConnectionString")
        {
        }
    }
}
