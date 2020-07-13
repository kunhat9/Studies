using CORE.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Internal;
namespace CORE.Services
{
    public class TB_TRANSACTIONFactory
    {
        public bool Insert(TB_TRANSACTION trans)
        {
            return new TB_TRANSACTIONSql().Insert(trans);
        }
        public bool Update(TB_TRANSACTION trans)
        {
            return new TB_TRANSACTIONSql().Update(trans);
        }
        public TB_TRANSACTION GetById(int id)
        {
            return new TB_TRANSACTIONSql().SelectByPrimaryKey(id);
        }
        public List<TB_TRANSACTION> GetAll()
        {
            return new TB_TRANSACTIONSql().SelectAll();
        }
        public List<TB_TRANSACTION> GetByUserId(int userId)
        {
            return new TB_TRANSACTIONSql().FilterByField("TransUserId", userId);
        }
    }
}
