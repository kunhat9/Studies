using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
    internal class TB_SUBJECTSSql : DataAccessTable<TB_SUBJECTS>
    {
        public TB_SUBJECTSSql() : base("Studies.ConnectionString")
        {
        }
    }
}
