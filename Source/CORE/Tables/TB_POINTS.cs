using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_POINTS : BusinessObject
    {
        [PrimaryKey]
        public int PointId { get; set; }
        public decimal PointNumber { get; set; }
        public int PointClassId { get; set; }
        public string PointType { get; set; }
        public TB_POINTS() { }
    }
}
