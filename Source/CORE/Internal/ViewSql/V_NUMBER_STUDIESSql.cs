using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_NUMBER_STUDIESSql : DataAccessTable<V_NUMBER_STUDIES>
    {
        public V_NUMBER_STUDIESSql() : base("Studies.ConnectionString")
        {
        }
    }
}
