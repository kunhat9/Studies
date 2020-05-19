using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
    internal class TB_BOX_SUBJECTSSql : DataAccessTable<TB_BOX_SUBJECTS>
    {
        public TB_BOX_SUBJECTSSql() : base("Studies.ConnectionString")
        {
        }
    }
}
