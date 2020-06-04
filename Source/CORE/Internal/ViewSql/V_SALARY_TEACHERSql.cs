using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class V_SALARY_TEACHERSql : DataAccessTable<V_SALARY_TEACHER>
    {
        public V_SALARY_TEACHERSql() : base("Studies.ConnectionString")
        {
        }
    }
}
