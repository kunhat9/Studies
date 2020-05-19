using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{

    internal class TB_REGISTERSSql : DataAccessTable<TB_REGISTERS>
    {
        public TB_REGISTERSSql() : base("Studies.ConnectionString")
        {
        }
    }
}
