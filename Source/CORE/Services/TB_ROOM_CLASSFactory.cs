using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_ROOM_CLASSFactory
    {
        public List<TB_ROOM_CLASS> GetAll()
        {
            return new TB_ROOM_CLASSSql().SelectAll();
        }
        public TB_ROOM_CLASS GetById(int id)
        {
            return new TB_ROOM_CLASSSql().SelectByPrimaryKey(id);
        }
    }
}
