using MahApps.Metro.Controls;
using Park.Pages;
using Park.UserControls;
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
using Abp.Dependency;
using Castle.MicroKernel.Registration;

namespace Park.Froms
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow:ITransientDependency
    {
        private MainWindowViewModel mainWindowViewModel;

        LeftMenuControl control;
        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel();

            Fram_Content.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            IocManager.Instance.IocContainer.Register(Component.For<MainWindowViewModel>().Instance(mainWindowViewModel).LifestyleSingleton()); //注册mainwindowViewMOdel
            List<Menu> menus = new List<Menu>();
            menus.Add(new Menu() { IsOpen = false, Title = "首页", IsFull = true, PageType = typeof(Index) });
            menus.Add(new Menu()
            {
                IsOpen = false,
                ParentDefultIndex=0,
                Title = "车场基础信息",
                Menus = new Menu[]{
                new Menu {  Title= "车场基本信息",IsSelectedParkIn=true,PageType=typeof(Pages.ParkInfo)  },
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
                new Menu {  Title= "车场基本信息"},
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
            mainWindowViewModel.Menus = menus;
            control = new LeftMenuControl(mainWindowViewModel);
            control.ItemClickEventHandler += new RoutedEventHandler(MenuItemClick);
            Dkp_LeftMenu.Children.Add(control);
        }

        private void MenuItemClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Menu menu = sender as Menu;
            if (menu.PageType == null&&!menu.ParentDefultIndex.HasValue)
                return;
            else
            {
                if (menu.Menus != null && menu.ParentDefultIndex.HasValue)
                {
                    menu.Menus[menu.ParentDefultIndex.Value].IsOpen = !menu.IsOpen;
                    menu.IsFull = menu.Menus[menu.ParentDefultIndex.Value].IsFull;
                    menu.IsSelectedParkIn = menu.Menus[menu.ParentDefultIndex.Value].IsSelectedParkIn;
                    menu.PageType = menu.Menus[menu.ParentDefultIndex.Value].PageType;
                }
            }
            if (menu.IsFull)
            {
                Fram_Content.Margin = new Thickness(0);
            }
            else
            {
                Fram_Content.Margin = new Thickness(20, 25, 20, 0);
            }
            if (menu.IsSelectedParkIn && mainWindowViewModel.SelectParkDto == null)
            {
                Fram_Content.Navigate(IocManager.Resolve<Index>());
                menu.IsOpen = false;
                var first = mainWindowViewModel.Menus.First();
                first.IsOpen = true;
                return;
            }

            var type = menu.PageType;
            var page = IocManager.Resolve(type);
            Fram_Content.Navigate(page);
        }
    }
}
