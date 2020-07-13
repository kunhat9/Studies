using CORE.Internal;
using CORE.Tables;
using CORE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Services
{
    public class TB_REGISTERSFactory
    {
        public string Insert(TB_REGISTERS register)
        {
            string result = "";
            List<V_NUMBER_STUDIES> list = new List<V_NUMBER_STUDIES>();
            list = new TB_SCHEDULESFactory().GetCountStudieInClass(register.RegisterScheduleId.ToString());
            if(list.Count > 0)
            {
                if(list[0].CountStudie >= 20)
                {
                    result = "150";
                }else
                {
                    List<TB_REGISTERS> listRegis = new List<TB_REGISTERS>();
                    listRegis = new TB_REGISTERSSql().FilterByField("RegisterUserId", register.RegisterUserId).Where(x => x.RegisterScheduleId == register.RegisterScheduleId).ToList();
                    if(listRegis.Count == 0)
                    {
                        if (new TB_REGISTERSSql().Insert(register))
                        {
                            result = "00";
                        }
                        else
                        {
                            result = "200";
                        }
                    }
                    else
                    {
                        result = "300";
                    }
                 
                }
            }
            return result;
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
