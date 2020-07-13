using CORE.Tables;
using CORE.Base;

namespace CORE.Internal
{
   
    internal class TB_TRANSACTIONSql : DataAccessTable<TB_TRANSACTION>
    {
        public TB_TRANSACTIONSql() : base("Studies.ConnectionString")
        {
        }
    }
}
