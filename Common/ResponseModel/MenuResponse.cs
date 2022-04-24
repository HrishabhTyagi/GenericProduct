using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ResponseModel
{
    public class MenuHelper
    {
        public string MenuName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int ParentId { get; set; } = 0;
        public List<MenuHelper> SubMenuList { get; set; }
        public int MenuId { get; set; }
    }

    public class MenuResponse
    {
        public string MenuName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<MenuResponse> SubMenuList { get; set; }
    }
}
