﻿using System;
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
using System.Windows.Threading;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Park.Childers;
using Park.ParkBox.Dto;
using Park.Parks.Park;
using Park.UserControls;

namespace Park.Pages
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Page,ITransientDependency
    {
        private readonly IParkAppService _parkAppService;

        private  ParkDto selectedPark;
        IReadOnlyList<ParkDto> parkDtos;

         private DelayAction delayAction;
        public Index(IParkAppService parkAppService)
        {
            InitializeComponent();
            _parkAppService = parkAppService;
            delayAction = new DelayAction();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await _parkAppService.GetAll(new Abp.Application.Services.Dto.PagedResultRequestDto() { MaxResultCount = 7, SkipCount = 0 });
             AddShowPanel(result);
        }

        private  void AddShowPanel(PagedResultDto<ParkDto> result)
        {
         
            parkDtos = result.Items;

            ParkInfo parkInfo;
            var width = (this.ActualWidth - 5 * 80) / 4;
            foreach (var park in parkDtos)
            {
                parkInfo = new ParkInfo();
                parkInfo.Margin = new Thickness(80, 100, 0, 0);
                parkInfo.Width = width;  //每行展示4个
                parkInfo.Height = 250;
                parkInfo.DataContext = park;
                parkInfo.MouseLeftButtonDown += ParkInfo_MouseLeftButtonDown;
                ShowPanel.Children.Add(parkInfo);
            }
            var parkAdd = new UserControl();
            parkAdd.Width = width;
            parkAdd.Height = 250;

            parkAdd.Margin = new Thickness(80, 100, 0, 0);
            parkAdd.Template = (ControlTemplate)FindResource("AddParkControlTemplate");
            parkAdd.MouseLeftButtonDown += ParkAdd_MouseLeftButtonDown;
            ShowPanel.Children.Add(parkAdd);
        }

        private async  void ParkAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddPark addPark = new AddPark();
            var result = addPark.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var list = await _parkAppService.GetAll(new Abp.Application.Services.Dto.PagedResultRequestDto() { MaxResultCount = 7, SkipCount = 0 });
                ShowPanel.Children.Clear();
                AddShowPanel(list);
            }
        }

        private void ParkInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ParkInfo parkInfo = sender as ParkInfo;
            var park = parkInfo.DataContext as ParkDto;
            park.IsSelected = true;

            if (selectedPark != null && selectedPark != park)
            {
                selectedPark.IsSelected = false;
            }
            selectedPark = park;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            delayAction.Debounce(500, DispatcherSynchronizationContext.Current,async () =>
            {
                var result = await _parkAppService.GetParkListByName(new PagedResultRequestDto() { MaxResultCount = 7, SkipCount = 0 }, Txt_ParkName.Text);
                ShowPanel.Children.Clear();
                selectedPark = null;
                AddShowPanel(result);
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow as MetroWindow;
            if (selectedPark == null)
            {
                await main.ShowMessageAsync("提示", "请选择要删除的车场");
                return;
            }
            if (await main.ShowMessageAsync("提示", "请选择要删除的车场", MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
            {
                await _parkAppService.Delete(selectedPark);
                if (string.IsNullOrEmpty(Txt_ParkName.Text)){
                    var result = await _parkAppService.GetAll(new Abp.Application.Services.Dto.PagedResultRequestDto() { MaxResultCount = 7, SkipCount = 0 });
                    ShowPanel.Children.Clear();
                    AddShowPanel(result);
                }
                else
                {
                    Txt_ParkName.Text = "";
                }
            }

        }
        /// <summary>
        /// addPark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ParkAdd_MouseLeftButtonDown(null, null);
        }
    }
}
