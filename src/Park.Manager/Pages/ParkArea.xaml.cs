using Park.ParkAreases;
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
using Park.ParkBox.Dto;
using Park.ParkAreases.Dtos;
using Abp.ObjectMapping;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Park.Pages
{
    /// <summary>
    /// ParkArea.xaml 的交互逻辑
    /// </summary>
    public partial class ParkArea : Page,ISingletonDependency
    {
        private readonly IParkAreasAppService _parkAreasAppService;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IObjectMapper objectMapper;

        public ParkAreaViewModel ParkAreaViewModel;
        public ParkArea(IParkAreasAppService parkAreasAppService,MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _parkAreasAppService = parkAreasAppService;
            _mainWindowViewModel = mainWindowViewModel;
            ParkAreaViewModel = new ParkAreaViewModel();
            objectMapper = NullObjectMapper.Instance;
            this.DataContext = ParkAreaViewModel;
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            if (ParkAreaViewModel.SelectedParkArea != null && ParkAreaViewModel.SelectedParkArea.Id > 0)
            {
                var result = await window.ShowMessageAsync("提示", "确定要删除吗？", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative)
                    await _parkAreasAppService.DeleteParkAreas(ParkAreaViewModel.SelectedParkArea);
            }
            else
            {
                await window.ShowMessageAsync("提示", "请选择要删除的区域");
            }
        }
        /// <summary>
        /// save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ParkAreaViewModel.SelectedParkArea != null)
            {
                await _parkAreasAppService.CreateOrUpdateParkAreas(new CreateOrUpdateParkAreasInput() { ParkAreas = objectMapper.Map<ParkAreasEditDto>(ParkAreaViewModel.SelectedParkArea) });

            }
            else
            {
                var window = Application.Current.MainWindow as MetroWindow;
                await window.ShowMessageAsync("提示", "请选择要保存的区域");
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ParkAreaViewModel.TreeViewMenu = await _parkAreasAppService.GetParkAreaDtosGroupByParent(_mainWindowViewModel.SelectParkDto.Id);

            ParkAreaViewModel.ComboxData = await _parkAreasAppService.GetParkAreaAllParents(_mainWindowViewModel.SelectParkDto.Id);
            ParkAreaViewModel.ComboxData.Insert(0, new ParkAreaDto() { Id = 0, AreaName = "请选择" });
            trvFamilies.ItemsSource = ParkAreaViewModel.TreeViewMenu;
            Cmb_ParentArea.ItemsSource = ParkAreaViewModel.ComboxData;
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ParkAreaViewModel.SelectedParkArea = new ParkAreaDto();
        }

        private void trvFamilies_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ParkAreaViewModel.SelectedParkArea = (sender as TreeView).SelectedValue as ParkAreaDto;
        }
    }
}
