using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
    internal class TB_POINTSSql : DataAccessTable<TB_POINTS>
    {
        public TB_POINTSSql() : base("Studies.ConnectionString")
        {
        }
    }
}
