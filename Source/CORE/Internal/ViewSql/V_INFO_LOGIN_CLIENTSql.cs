using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_INFO_LOGIN_CLIENTSql : DataAccessTable<V_INFO_LOGIN_CLIENT>
    {
        public V_INFO_LOGIN_CLIENTSql() : base("Studies.ConnectionString")
        {
        }
    }
}
