using MahApps.Metro.Controls;
using Park.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Park.Froms
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Menu> menus = new List<Menu>();
            menus.Add(new Menu() { IsOpen = false, Title = "首页" });
            menus.Add(new Menu()
            {
                IsOpen = true,
                Title = "车场基础信息",
                Menus = new Menu[]{
                new Menu {  Title= "车场基本信息" },
                new Menu {  Title= "区域配置" },
                new Menu {  Title= "停车位" },
                new Menu {  Title= "岗亭设置" },
                }
            });

            menus.Add(new Menu()
            {
                IsOpen = false,
                Title = "车场基础信息",
                Menus = new Menu[]{
                new Menu {  Title= "车场基本信息" },
                new Menu {  Title= "区域配置" },
                new Menu {  Title= "停车位" },
                new Menu {  Title= "岗亭设置" },
                }
            });

            foreach (var item in menus)
            {
                if (item.Menus != null)
                {
                    foreach (var child in item.Menus)
                    {
                        child.Parent = item;
                    }
                }
            }

            lbx_Menus.ItemsSource = menus;
        }
    }
}
