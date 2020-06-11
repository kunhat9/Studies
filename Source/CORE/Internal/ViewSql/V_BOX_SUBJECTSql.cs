using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_BOX_SUBJECTSql : DataAccessTable<V_BOX_SUBJECT>
    {
        public V_BOX_SUBJECTSql() : base("Studies.ConnectionString")
        {
        }
    }
}
