using CORE.Base;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_INFO_LOGIN_CLIENT : BusinessObject
    {
        public string ecode { get; set; }
        public string edesc { get; set; }
        public TB_USERS user { get; set; }
    }
}
