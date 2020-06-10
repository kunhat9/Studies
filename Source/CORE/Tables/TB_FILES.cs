using CORE.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Tables
{
    public class TB_FILES : BusinessObject
    {
        [PrimaryKey]
        public int FileId { get; set; }
        public string FileUrl { get; set; }
        public string FileRef { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileService { get; set; }


    }
}
