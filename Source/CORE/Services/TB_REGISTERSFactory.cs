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
            // lay ra danh sach lich hoc cua hoc sinh
            List<V_TKBStudies> listTKB = new List<V_TKBStudies>();
            listTKB = new TB_USERSFactory().GetThoiKhoaBieu(register.RegisterUserId.ToString());
            // lay ra danh sach thu trong tuan
            List<TB_SCHEDULE_DETAILS> listDetails = new List<TB_SCHEDULE_DETAILS>();
            listDetails = new TB_SCHEDULE_DETAILSFactory().GetByScheduleId(register.RegisterScheduleId.ToString());
            // lay danh sach da dang ky nhung chua duoc xac nhan
            List<TB_REGISTERS> listRegisTem = new List<TB_REGISTERS>();
            listRegisTem = new TB_REGISTERSSql().FilterByField("RegisterUserId", register.RegisterUserId);
            bool check = true;
            foreach (var tkb in listRegisTem)
            {
                List<TB_SCHEDULE_DETAILS> listTemp = new List<TB_SCHEDULE_DETAILS>();
                listTemp = new TB_SCHEDULE_DETAILSFactory().GetByScheduleId(tkb.RegisterScheduleId.ToString());
                foreach(var a in listTemp)
                {
                    if(listDetails.Where(x=>x.ScheduleDetailId == a.ScheduleDetailId).ToList().Count ==0)
                    {
                        if (listDetails.Where(x => x.ScheduleDetailDayOfWeek.Equals(a.ScheduleDetailDayOfWeek) && x.ScheduleDetailTimeFrom.Hours == a.ScheduleDetailTimeFrom.Hours).ToList().Count > 0)
                        {
                            check = false;
                        }
                    }
                      
                }
            }

            
          
           
            if (check)
            {
                bool check2 = true;
                foreach (var tkb in listTKB)
                {
                    if (listDetails.Where(x => x.ScheduleDetailDayOfWeek.Equals(tkb.ScheduleDetailDayOfWeek) && x.ScheduleDetailTimeFrom.Hours == tkb.ScheduleDetailTimeFrom.Hours).ToList().Count > 0)
                    {
                        check2 = false;
                    }

                }
                if (check2)
                {
                    if (list.Count > 0)
                    {
                        if (list[0].CountStudie >= 20)
                        {
                            result = "150";
                        }
                        else
                        {
                            List<TB_REGISTERS> listRegis = new List<TB_REGISTERS>();
                            listRegis = new TB_REGISTERSSql().FilterByField("RegisterUserId", register.RegisterUserId).Where(x => x.RegisterScheduleId == register.RegisterScheduleId).ToList();
                            if (listRegis.Count == 0)
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
                }
                else
                {
                    result = "400";
                }
                
            }else
            {
                result = "400";
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
