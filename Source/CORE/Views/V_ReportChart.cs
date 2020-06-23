using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Views
{
    public class V_ReportChart : BusinessObject
    {
        public string NgayLamViec { get; set; }
        public decimal Thu { get; set; }
        public decimal Chi { get; set; }
        public V_ReportChart() { }
    }
}
