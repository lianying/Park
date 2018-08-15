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


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
