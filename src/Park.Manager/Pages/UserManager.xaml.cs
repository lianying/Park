using Park.CarUserses;
using Park.ViewModel;
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
using Abp.Dependency;
using Park.ParkAreases;
using Park.ParkEntranceses.Dtos;
using Park.CarUserGroups;
using Park.CarUserGroups.Dtos;
using Park.CarUserses.Dtos;
using Park.UserControls;
using System.ComponentModel;
using Park.EnumHelper;
using Park.CarNumberses.Dtos;
using Park.CarNumberses;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Abp.AutoMapper;
using Park.Childers;
using Park.CarPorts;

namespace Park.Pages
{
    /// <summary>
    /// UserManager.xaml 的交互逻辑
    /// </summary>
    public partial class UserManager : Page,ISingletonDependency
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly ICarUsersAppService _carUsersAppService;
        private readonly IParkAreasAppService _parkAreasAppService;
        private readonly ICarUserGroupAppService _carUserGroupAppService;
        private readonly ICarNumbersAppService _carNumbersAppService;
        private readonly ICarPortAppService _carPortAppService;
        private UserManagerViewModel _managerViewModel;

        public UserManager(MainWindowViewModel mainWindowViewModel,
            ICarUsersAppService carUsersAppService,
            IParkAreasAppService parkAreasAppService,
            ICarUserGroupAppService carUserGroupAppService,
            ICarNumbersAppService carNumbersAppService,
            ICarPortAppService carPortAppService
            )
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _carUsersAppService = carUsersAppService;
            _parkAreasAppService = parkAreasAppService;
            _carUserGroupAppService = carUserGroupAppService;
            _carNumbersAppService = carNumbersAppService;
            _carPortAppService = carPortAppService;
            _managerViewModel = new UserManagerViewModel();
            DataContext = _managerViewModel;
        }

        private void trvFamilies_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = sender as TreeView;
            if (treeView.SelectedValue is CarUsersListDto)
            {
                _managerViewModel.SelectDto = treeView.SelectedValue as CarUsersListDto;

                _managerViewModel.SelectedMyEnumType = new KeyValuePair<Enum.UserType, string>(_managerViewModel.SelectDto.UserType, _managerViewModel.SelectDto.UserType.GetAttributeOfType<DescriptionAttribute>().Description);
                LoadCarNumbersAndCarPorts();


            }
        }

        private void LoadCarNumbersAndCarPorts()
        {
            if (_managerViewModel.SelectDto == null)
            {
                return;
            }

            double carNumberWidth = ShowPanel_CarNumbers.ActualHeight;
            double carPortWidth = ShowPanel_CarPorts.ActualHeight;
            double width = (ShowPanel_CarNumbers.ActualWidth - 3 * 20) / 4;
            LoadCarNumber(width, carNumberWidth);
            LoadParkCarport(width, carPortWidth);

            LoadAddControl();
        }
        private void LoadParkCarport(double with, double height)
        {
            ShowPanel_CarPorts.Children.Clear();
            foreach (var item in _managerViewModel.SelectDto.CarPorts)
            {
                var carNumberInfo = new CarportInfo(_carPortAppService, _mainWindowViewModel, _managerViewModel, item);
                carNumberInfo.Height = height;
                carNumberInfo.Width = with;
                carNumberInfo.RoutedEventHandler += new
                     RoutedEventHandler(ReloadCarPort);
                carNumberInfo.Margin = new Thickness(0, 0, 20, 0);
                ShowPanel_CarPorts.Children.Add(carNumberInfo);
            }
        }

        private void LoadCarNumber(double with,double height) {
            ShowPanel_CarNumbers.Children.Clear();
            foreach (var item in _managerViewModel.SelectDto.CarNumbers)
            {
                var carNumberInfo = new CarNumberInfo(item);
                carNumberInfo.Height = height;
                carNumberInfo.Width = with;
                carNumberInfo.Margin = new Thickness(0, 0, 20, 0);
                carNumberInfo.DeleteClickEventHandler += new RoutedEventHandler(DeleteCarNumber);
                ShowPanel_CarNumbers.Children.Add(carNumberInfo);
            }
        }

        private async void ReloadCarPort(object sender, RoutedEventArgs e)
        {

            double carPortWidth = ShowPanel_CarPorts.ActualHeight;
            double width = (ShowPanel_CarNumbers.ActualWidth - 3 * 20) / 4;
            LoadParkCarport(width, carPortWidth);
        }
        /// <summary>
        /// 删除车牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteCarNumber(object sender,RoutedEventArgs e)
        {
            if (_managerViewModel.SelectDto == null)
            {
                return;
            }
            var carNumber = sender as CarNumbersListDto;
            await _carNumbersAppService.DeleteCarNumbers(carNumber);
            _managerViewModel.SelectDto.CarNumbers.Remove(carNumber);

            double carNumberWidth = ShowPanel_CarNumbers.ActualHeight;

            double width = (ShowPanel_CarNumbers.ActualWidth - 3 * 20) / 4;
            LoadCarNumber(width, carNumberWidth);

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadParkArea();
            _managerViewModel.GroupListDtos = new System.Collections.ObjectModel.ObservableCollection<CarUserGroupListDto>(await _carUserGroupAppService.GetCarUserGroupListDtos(_mainWindowViewModel.SelectParkDto.Id));
            ShowPanel_CarNumbers.Children.Clear();
            ShowPanel_CarPorts.Children.Clear();
            LoadAddControl();
            _managerViewModel.SelectDto = new CarUsersListDto();
            
        }
        /// <summary>
        /// 加载车辆 车位添加按钮
        /// </summary>
        private void LoadAddControl()
        {
            double carNumberWidth = ShowPanel_CarNumbers.ActualHeight;
            double carPortWidth = ShowPanel_CarPorts.ActualHeight;

            double width = (ShowPanel_CarNumbers.ActualWidth - 3 * 20) / 4;
            var carNumberAdd = new UserControl();
            carNumberAdd.Width = width;
            carNumberAdd.Height = carNumberWidth;

            carNumberAdd.Margin = new Thickness(0, 0, 20, 0);
            carNumberAdd.Template = (ControlTemplate)FindResource("AddParkControlTemplate");
            carNumberAdd.MouseLeftButtonDown += CarNumberAdd_MouseLeftButtonDown; ;
            ShowPanel_CarNumbers.Children.Add(carNumberAdd);


            
            var carPortControl = new UserControl();
            carPortControl.Width = width;
            carPortControl.Height = carNumberWidth;

            carPortControl.Margin = new Thickness(0, 0, 20, 0);
            carPortControl.Template = (ControlTemplate)FindResource("AddParkControlTemplate");
            carPortControl.MouseLeftButtonDown += CarPortControl_MouseLeftButtonDown; ; ;
            ShowPanel_CarPorts.Children.Add(carPortControl);
        }

        /// <summary>
        /// 添加车位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CarPortControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_managerViewModel.SelectDto != null && _managerViewModel.SelectDto.Id.Value > 0)
            {
                AddCarport addCarport = new AddCarport(_mainWindowViewModel, _carPortAppService, _managerViewModel.SelectDto);
                var result = addCarport.ShowDialog();
                if (result.HasValue && result.Value)
                {

                    _managerViewModel.SelectDto.CarPorts = await _carPortAppService.GetCarPortListDtosByUserId(_managerViewModel.SelectDto.Id.Value);
                    double carPortWidth = ShowPanel_CarPorts.ActualHeight;
                    double width = (ShowPanel_CarNumbers.ActualWidth - 3 * 20) / 4;

                    LoadParkCarport(width, carPortWidth);
                }
            }
            else
            {

                var window = Application.Current.MainWindow as MetroWindow;
                await window.ShowMessageAsync("提示", "请先保存当前用户");
            }
        }

        /// <summary>
        /// 添加车牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CarNumberAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            if (_managerViewModel.SelectDto != null && _managerViewModel.SelectDto.Id > 0)
            {
                AddCarNumber addCarNumber = new AddCarNumber(_managerViewModel, _carNumbersAppService);
                var result = addCarNumber.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    _managerViewModel.SelectDto.CarNumbers = await _carNumbersAppService.GetCarNumbersListDtosByUserId(_managerViewModel.SelectDto.Id.Value);
                    double carPortWidth = ShowPanel_CarNumbers.ActualHeight;
                    double width = (ShowPanel_CarNumbers.ActualWidth - 3 * 20) / 4;
                    LoadParkCarport(width, carPortWidth);
                }
            }
            else
            {

                var window = Application.Current.MainWindow as MetroWindow;
                await window.ShowMessageAsync("提示", "请先保存当前用户");
            }
        }

        private async void LoadParkArea()
        {

            var parkAea = await _parkAreasAppService.GetParkAreaDtos(_mainWindowViewModel.SelectParkDto.Id);
            //trvFamilies.ItemsSource = parkAea;
            List<CarUserGroupListDto> list;
            foreach (var item in parkAea)
            {
                list = await _carUserGroupAppService.GetCarUserGroupListDtosByAreaId(item.Id);
                foreach (var userGroup in list)
                {
                    var temp = await _carUsersAppService.GetCarUsersListDtosByGroupId(userGroup.Id);
                    userGroup.UsersListDtos = new System.Collections.ObjectModel.ObservableCollection<CarUserses.Dtos.CarUsersListDto>(temp);
                }


                item.GroupListDtos = new System.Collections.ObjectModel.ObservableCollection<CarUserGroupListDto>(list);
            }

            _managerViewModel.ParkAreaDtos = new System.Collections.ObjectModel.ObservableCollection<ParkAreases.Dtos.ParkAreaDto>(parkAea);

        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            var window = Application.Current.MainWindow as MetroWindow;
            var selectDto = _managerViewModel.SelectDto;
            
            await _carUsersAppService.CreateOrUpdateCarUsers(new CreateOrUpdateCarUsersInput() { CarUsers = selectDto});

            await window.ShowMessageAsync("提示", "操作成功！");
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var window = Application.Current.MainWindow as MetroWindow;
            if (_managerViewModel.SelectDto != null && _managerViewModel.SelectDto.Id > 0)
            {
                await _carUsersAppService.DeleteCarUsers(_managerViewModel.SelectDto);
            }
            else {

                await window.ShowMessageAsync("提示", "请选择要删除的用户");
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _managerViewModel.SelectDto = new CarUsersListDto();
            ShowPanel_CarNumbers.Children.Clear();
            ShowPanel_CarPorts.Children.Clear();
            LoadAddControl();
        }
        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddCarUserGroup addCarUserGroup = new AddCarUserGroup(_carUserGroupAppService);
            var result = addCarUserGroup.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _managerViewModel.GroupListDtos = new System.Collections.ObjectModel.ObservableCollection<CarUserGroupListDto>(await _carUserGroupAppService.GetCarUserGroupListDtos(_mainWindowViewModel.SelectParkDto.Id));
            }

        }
    }
}
