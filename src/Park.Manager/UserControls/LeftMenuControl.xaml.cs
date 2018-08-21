using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Park.UserControls
{
    /// <summary>
    /// LeftMenuControl.xaml 的交互逻辑
    /// </summary>
    public partial class LeftMenuControl : UserControl
    {
        private List<Park.ViewModel.Menu> _menus;

        public RoutedEventHandler ItemClickEventHandler;
        public LeftMenuControl( List<Park.ViewModel.Menu> menus)
        {
            InitializeComponent();
            _menus = menus;
            lbx_Menus.ItemsSource = _menus;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var control = sender as StackPanel;
            var menu = control.DataContext as Park.ViewModel.Menu;
            ItemClickEventHandler?.Invoke(menu, e);
            if (menu.Parent != null)
            {
                foreach (var item in menu.Parent.Menus)
                {
                    item.IsOpen = false;
                }
            }
            else
            {
                foreach (var item in _menus)
                {
                    item.IsOpen = false;
                } 
            }
            menu.IsOpen = !menu.IsOpen;
           
        }

        
    }
}
