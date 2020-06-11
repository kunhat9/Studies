using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_CLASSSql : DataAccessTable<V_CLASS>
    {
        public V_CLASSSql() : base("Studies.ConnectionString")
        {
        }
    }
}
