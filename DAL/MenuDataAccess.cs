using Common.ProductContant;
using Common.ResponseModel;
using SqlServerEntity.EntityModel;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class MenuDataAccess : BaseDataAccess
    {
        /// <summary>
        /// Get Menu List
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<Menu> GetMenuList(int roleId)
        {
            if (roleId == (int)RoleConstant.Administrator)
            {
                var allMenu = _context.Menus.Where(a => a.IsActive??true).ToList();

                return _context.Menus.ToList();
            }
            else
                return null;
        }
        public List<Menu> GetMenuList()
        {
          
                return _context.Menus.Where(a => a.IsActive ?? true).ToList();

          
        }
    }
}
