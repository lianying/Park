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
            mainWindowViewModel = IocManager.Resolve<MainWindowViewModel>();

            Fram_Content.NavigationUIVisibility = NavigationUIVisibility.Hidden;
           // IocManager.Instance.IocContainer.Register(Component.For<MainWindowViewModel>().Instance(mainWindowViewModel).LifestyleSingleton()); //注册mainwindowViewMOdel
            List<Menu> menus = new List<Menu>
            {
                new Menu() { IsOpen = false, Title = "首页", IsFull = true, PageType = typeof(Index), Icon = "/icon/icon001.png" },
                new Menu()
                {
                    IsOpen = false,
                    ParentDefultIndex = 0,
                    Title = "车场基础信息",
                    Icon = "/icon/icon002.png",
                    Menus = new Menu[]{
                new Menu {  Title= "车场基本信息",IsSelectedParkIn=true,PageType=typeof(Pages.ParkInfo)  },
                new Menu {  Title= "区域配置",IsSelectedParkIn=true,PageType=typeof(ParkArea) },
                new Menu {  Title= "停车位",IsSelectedParkIn=true,PageType=typeof(ParkCarport) },
                new Menu {  Title= "出入口",IsSelectedParkIn=true,PageType=typeof(BackList) },
                new Menu {  Title= "岗亭设置",IsSelectedParkIn=true,PageType=typeof(Pages.ParkBox) },
                }
                },

                new Menu()
                {
                    IsOpen = false,
                    Title = "车主账户设置",
                    Icon = "/icon/icon003.png",
                    Menus = new Menu[]{
                new Menu {  Title= "用户管理",IsSelectedParkIn=true,PageType=typeof(Pages.UserManager)},
                new Menu {  Title= "免费用户",IsSelectedParkIn=true,PageType=typeof(FreeUser) },
                new Menu {  Title= "车位费催缴",PageType=typeof(ParkingFeeCollection) },
                }
                },
                new Menu()
                {
                    IsOpen = false,
                    ParentDefultIndex = 0,
                    Title = "车场设备设置",
                    Icon = "/icon/icon003.png",
                    Menus = new Menu[]{
                new Menu {  Title= "摄像机",IsSelectedParkIn=true,PageType=typeof(CameraManager)  },
                new Menu {  Title= "显示屏",IsSelectedParkIn=true,PageType=typeof(LedManager) },
                new Menu {  Title= "收费标准",IsSelectedParkIn=true,PageType=typeof(FareRuleManager) },
                }
                },

                new Menu()
                {
                    IsOpen = false,
                    ParentDefultIndex = 0,
                    Title = "报表相关",
                    Icon = "/icon/icon004.png",
                    Menus = new Menu[]{
                new Menu {  Title= "操作明细",IsSelectedParkIn=true,PageType=typeof(OperationDetails)  },
                new Menu {  Title= "进出场报表",IsSelectedParkIn=true,PageType=typeof(InOutReport) },
                new Menu {  Title= "场内车辆报表",IsSelectedParkIn=true,PageType=typeof(CarInReport) },
                new Menu {  Title= "收费报表",IsSelectedParkIn=true,PageType=typeof(ChargeDetails) },
                new Menu {  Title= "车牌修改记录",IsSelectedParkIn=true,PageType=typeof(EditCarNumberReport) },
                new Menu {  Title= "充值延期报表",IsSelectedParkIn=true,PageType=typeof(RechargeReport) },
                new Menu {  Title= "异常出入场报表",IsSelectedParkIn=true,PageType=typeof(ErrorInOutReport) },
                }
                },
                new Menu()
                {
                    IsOpen = false,
                    ParentDefultIndex = 0,
                    Title = "系统管理",
                    Icon = "/icon/icon005.png",
                    Menus = new Menu[]{
                new Menu {  Title= "用户设置",IsSelectedParkIn=true,PageType=typeof(SysUserManager)  },
                new Menu {  Title= "系统设置",IsSelectedParkIn=true,PageType=typeof(SysSetting) },
                new Menu {  Title= "车类型",IsSelectedParkIn=true,PageType=typeof(CarTypeManager) },
                new Menu {  Title= "班次设置",IsSelectedParkIn=true,PageType=typeof(ShiftManager) },
                new Menu {  Title= "黑名单",IsSelectedParkIn=true,PageType=typeof(BackLisReportt) }
                }
                }
            };
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
            DataContext = mainWindowViewModel;
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
