using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_USER_TRACKEDSql : DataAccessTable<V_USER_TRACKED>
    {
        public V_USER_TRACKEDSql() : base("Studies.ConnectionString")
        {
        }
    }
}
