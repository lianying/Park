using Abp.Application.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Navigation
{
    public static class MenuExtenstion
    {
        public static NgMenu UserMenuToNgMenu(this UserMenu menu)
        {
            var ngMenu = new NgMenu();
            ngMenu.hide = false;
            ngMenu.text = menu.Name;
            ngMenu.group = true;
            ngMenu.children = Array.ConvertAll<UserMenuItem, NgMenu>(menu.Items.ToArray(), x => x.MenuItemToNgMenu());
            ngMenu.i18n = string.Empty;
            ngMenu.acl = string.Empty;
            ngMenu.link = string.Empty;
            return ngMenu;
        }

        public static NgMenu MenuItemToNgMenu(this UserMenuItem userMenuItem)
        {
            if (userMenuItem == null)
                return null;
            var menu = new NgMenu();
            menu.text = userMenuItem.Name;
            menu.link = userMenuItem.Url;
            menu.icon = userMenuItem.Icon;
            menu.hide = !userMenuItem.IsVisible;
            menu.target = userMenuItem.Target;
            menu.i18n = string.Empty;
            menu.acl = string.Empty;
            menu.children = Array.ConvertAll<UserMenuItem, NgMenu>(userMenuItem.Items.ToArray(), x => x.MenuItemToNgMenu());


            return menu;
        }
    }
}
