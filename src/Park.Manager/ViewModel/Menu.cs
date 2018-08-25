using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class Menu: INotifyPropertyChanged
    {
        public Menu Parent { get; set; }
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public Object Tag { get; set; }

        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
               
                    isOpen = value;
                NotifyPropertyChanged("IsOpen");
            }
        }

        private string icon;
        public string Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                NotifyPropertyChanged("Icon");
            }
        }


        public Type PageType { get; set; }

        public Menu[] Menus { get; set; }


        public bool IsFull { get; set; }

        /// <summary>
        /// 父级菜单默认关联子项
        /// </summary>
        public int? ParentDefultIndex { get; set; }

        public bool IsSelectedParkIn { get; set; }


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            if(obj is Menu)
            {
                var menu = obj as Menu;
                return menu.Title == Title && menu.Icon == Icon;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Title.GetHashCode() + Icon?.GetHashCode() ?? 0;
        }
    }
}
