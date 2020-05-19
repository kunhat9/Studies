using System.Collections.Generic;

namespace WebAdmin.Models
{
    public class ACTION_TREE
    {
        public string ACTION_ID { get; set; }

        public string ACTION_NAME { get; set; }

        public bool ACTION_ISMODULE { get; set; }

        public bool ACTION_ISROOT { get; set; }

        public bool ACTION_ISSHOW { get; set; }

        public int ACTION_ORDER { get; set; }

        public string ACTION_TYPE { get; set; }

        public string ACTION_CONTROLPATH { get; set; }

        public string ACTION_DESCRIPTION { get; set; }
        public string ACTION_ICON_CLASS { get; set; }

        public string ACTION_PARENT_ID { get; set; }

        public int IS_SELECTED { get; set; }
        public ACTION_TREE() { }

       
    }
}