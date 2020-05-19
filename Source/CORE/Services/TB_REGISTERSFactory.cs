using CORE.Internal;
using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_REGISTERSFactory
    {
        public bool Insert(TB_REGISTERS register)
        {
            return new TB_REGISTERSSql().Insert(register);
        }
        public bool Update(TB_REGISTERS register)
        {
            return new TB_REGISTERSSql().Update(register);
        }
        public bool Delete(int registerId)
        {
            return new TB_REGISTERSSql().Delete(registerId);
        }
        public List<TB_REGISTERS> GetAll()
        {
            return new TB_REGISTERSSql().SelectAll();
        }
        public TB_REGISTERS GetById(int registerId)
        {
            return new TB_REGISTERSSql().SelectByPrimaryKey(registerId);
        }
    }
}
