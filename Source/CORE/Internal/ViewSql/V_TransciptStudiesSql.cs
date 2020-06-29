using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_TransciptStudiesSql : DataAccessTable<V_TransciptStudies>
    {
        public V_TransciptStudiesSql() : base("Studies.ConnectionString")
        {
        }
    }
}
