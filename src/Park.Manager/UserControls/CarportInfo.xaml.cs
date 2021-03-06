﻿using Abp.AutoMapper;
using Park.CarPorts;
using Park.CarPorts.Dtos;
using Park.Childers;
using Park.Childers.ViewModels;
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

namespace Park.UserControls
{
    /// <summary>
    /// CarportInfo.xaml 的交互逻辑
    /// </summary>
    public partial class CarportInfo : UserControl
    {

        private MainWindowViewModel _mainWindowViewModel;
        private CarPortTimeExTenstionViewModel _carPortTimeExTenstionViewModel;
        private readonly ICarPortAppService _carPortAppService;
        private UserManagerViewModel _userManagerViewModel;
        private CarPortListDto _carPortListDto;
        public RoutedEventHandler RoutedEventHandler;
        public CarportInfo(ICarPortAppService carPortAppService, MainWindowViewModel mainWindowViewModel, UserManagerViewModel userManagerViewModel, CarPortListDto carPortListDto)
        {
            InitializeComponent();
            _carPortListDto = carPortListDto;
            _carPortAppService = carPortAppService;
            _mainWindowViewModel = mainWindowViewModel;
            _userManagerViewModel = userManagerViewModel;
            _carPortTimeExTenstionViewModel = new CarPortTimeExTenstionViewModel();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CarPortTimeExtension carPortTimeExtension = new CarPortTimeExtension(_carPortAppService, _mainWindowViewModel, _userManagerViewModel, _carPortListDto);
            var result = carPortTimeExtension.ShowDialog();
            if (result.HasValue && result.Value)
            {
                RoutedEventHandler?.Invoke(sender, e);
            }
            
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            await _carPortAppService.RemoveCarUserId(new CreateOrUpdateCarPortInput() { CarPort = _carPortListDto.MapTo<CarPortEditDto>() });
            _userManagerViewModel.SelectDto.CarPorts.Remove(_carPortListDto);
            RoutedEventHandler?.Invoke(sender, e);
        }
    }
}
