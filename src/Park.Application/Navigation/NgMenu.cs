using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Navigation
{
    public class NgMenu
    { 
        public bool group { get; set; }
        /** 文本 */
        public string text { get; set; }
        /** angular 路由 */
        public string link { get; set; }
        /** 链接 target   '_blank' | '_self' | '_parent' | '_top' */
        public string target { get; set; }
        /** 图标 */
        public string icon { get; set; }
        /** 是否隐藏菜单 */
        public bool hide { get; set; }
        /** i18n主键 */
        public string i18n { get; set; }
        /** 是否快捷菜单项 */
        public bool shortcut { get; set; }
        /** ACL配置，若导入 `@delon/acl` 时自动有效 */
        public object acl { get; set; }

        public NgMenu[] children { get; set; }
        

    }
}
