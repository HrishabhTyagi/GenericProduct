using Common.ResponseModel;
using DAL;
using SqlServerEntity.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAL
{
    public class MenuBusiness : BaseBusiness
    {
        private readonly MenuDataAccess menuDataAccess;

        public MenuBusiness()
        {
            menuDataAccess = new MenuDataAccess();
        }

        private List<MenuHelper> SortedMenu(List<MenuHelper> menuList)
        {
            List<MenuHelper> CopiedMenu = new List<MenuHelper>(menuList);

            List<MenuHelper> sortedMenu = new List<MenuHelper>();

            // Add all menu those don't have parent
            sortedMenu.AddRange(CopiedMenu.Where(m => m.ParentId == 0));

            CopiedMenu.RemoveAll(a => a.ParentId == 0);

            GenerateMenu(sortedMenu, CopiedMenu);

            return sortedMenu;
        }

        private void GenerateMenu(List<MenuHelper> sortedMenu, List<MenuHelper> copiedMenu)
        {
            if (sortedMenu.Count > 0)
            {
                foreach (var menu in sortedMenu)
                {
                    menu.SubMenuList = copiedMenu.Where(a => a.ParentId == menu.MenuId).ToList();
                    copiedMenu.RemoveAll(a => a.ParentId == menu.MenuId);

                    if (menu.SubMenuList.Count > 0)
                        GenerateMenu(menu.SubMenuList, copiedMenu);
                }
            }
        }

        /// <summary>
        /// Get Menu List
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<MenuResponse> GetMenuList(int roleId)
        {
            var menuList = menuDataAccess.GetMenuList(roleId);

            var menuListResponse = ListMapping<Menu, MenuHelper>(menuList);

            // Sorted menu
            var sortedMenu = SortedMenu(menuListResponse);

            //Convert MenuHelper model to MenuResponse model
            var convertedMenu = ListMapping<MenuHelper, MenuResponse>(sortedMenu);

            return convertedMenu;
        }
    }
}
