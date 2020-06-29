using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_TuitionStudiesSql : DataAccessTable<V_TuitionStudies>
    {
        public V_TuitionStudiesSql() : base("Studies.ConnectionString")
        {
        }
    }
  
}
