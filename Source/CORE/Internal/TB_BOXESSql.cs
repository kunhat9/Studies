using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
    internal class TB_BOXESSql : DataAccessTable<TB_BOXES>
    {
        public TB_BOXESSql() : base("Studies.ConnectionString")
        {
        }
    }
}
