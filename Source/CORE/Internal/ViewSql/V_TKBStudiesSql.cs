using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_TKBStudiesSql : DataAccessTable<V_TKBStudies>
    {
        public V_TKBStudiesSql() : base("Studies.ConnectionString")
        {
        }
    }
}
