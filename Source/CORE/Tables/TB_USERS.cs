using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_USERS : BusinessObject
    {
        [PrimaryKey]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
        public string UserStatus { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public DateTime UserDateCreated { get; set; }
        public string UserAcademicLevel { get; set; }
        public decimal UserNumberSalary { get; set; }
        public string UserNote { get; set; }
        public string UserFilesId { get; set; }

        public TB_USERS() { }

    }
}
