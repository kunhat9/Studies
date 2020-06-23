using CORE.Internal;
using CORE.Internal.ViewSql;
using CORE.Tables;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{

    public class TB_HEADQUARTERSFactory
    {
        public bool Insert(TB_HEADQUARTERS headquaters)
        {
            return new TB_HEADQUARTERSSql().Insert(headquaters);
        }
        public bool Update(TB_HEADQUARTERS headquaters)
        {
            return new TB_HEADQUARTERSSql().Update(headquaters);
        }
        public bool Delete(int id)
        {
            return new TB_HEADQUARTERSSql().Delete(id);
        }
        public List<TB_HEADQUARTERS> GetAll()
        {
            return new TB_HEADQUARTERSSql().SelectAll();
        }
        public TB_HEADQUARTERS GetById(int id)
        {
            return new TB_HEADQUARTERSSql().SelectByPrimaryKey(id);
        }
       
    }
}
