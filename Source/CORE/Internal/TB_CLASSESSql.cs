using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
    internal class TB_CLASSESSql : DataAccessTable<TB_CLASSES>
    {
        public TB_CLASSESSql() : base("Studies.ConnectionString")
        {
        }
    }
}
