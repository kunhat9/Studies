﻿using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Internal
{
    internal class TB_FILESSql : DataAccessTable<TB_FILES>
    {
        public TB_FILESSql() : base("Studies.ConnectionString")
        {
        }
    }
}
