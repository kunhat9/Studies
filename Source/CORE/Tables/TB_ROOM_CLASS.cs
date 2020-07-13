using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_ROOM_CLASS : BusinessObject
    {
        [PrimaryKey]
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public TB_ROOM_CLASS() { }
    }
}
