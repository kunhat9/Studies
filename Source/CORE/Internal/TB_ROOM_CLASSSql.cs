using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
    internal class TB_ROOM_CLASSSql : DataAccessTable<TB_ROOM_CLASS>
    {
        public TB_ROOM_CLASSSql() : base("Studies.ConnectionString")
        {
        }
    }
}
