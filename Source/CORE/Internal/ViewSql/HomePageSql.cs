using CORE.Base;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal.ViewSql
{
    internal class HomePageSql : DataAccessTable<HomePage>
    {
        public HomePageSql() : base("Studies.ConnectionString")
        {
        }
    }
}
