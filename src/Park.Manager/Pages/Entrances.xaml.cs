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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Park.CarTypeses;
using Park.ParkAreases;
using Park.ParkEntranceses;
using Park.ParkEntranceses.Dtos;
using Park.ViewModel;

namespace Park.Pages
{
    /// <summary>
    /// Entrances.xaml 的交互逻辑
    /// </summary>
    public partial class Entrances : Page,ISingletonDependency
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IParkEntrancesAppService _parkEntrancesAppService;
        private readonly ICarTypesAppService _carTypesAppService;
        private readonly IParkAreasAppService _parkAreasAppService;
        private ParkEntranceViewModel _entranceViewModel;
        public Entrances(MainWindowViewModel mainWindowViewModel, IParkEntrancesAppService parkEntrancesAppService,
            ICarTypesAppService carTypesAppService,
            IParkAreasAppService parkAreasAppService)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _parkEntrancesAppService = parkEntrancesAppService;
            _carTypesAppService = carTypesAppService;
            _entranceViewModel = new ParkEntranceViewModel();
            _parkAreasAppService = parkAreasAppService;
            this.DataContext = _entranceViewModel;
        }
        /// <summary>
        /// add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel_MouseLeftButtonDown(sender, null);
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow as MetroWindow;
            if (_entranceViewModel.SelectDto == null)
            {
                window.ShowMessageAsync("提示", "请选择要删除的出入口");
                return;
            }
            _parkEntrancesAppService.DeleteParkEntrances(_entranceViewModel.SelectDto);
            LoadParkArea();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _entranceViewModel.SelectDto = new ParkEntrancesListDto();
        }

        private void trvFamilies_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeView = sender as TreeView;
            if (treeView.SelectedValue is ParkEntranceses.Dtos.ParkEntrancesListDto) //选中的为出入口
            {
                _entranceViewModel.SelectDto = treeView.SelectedValue as ParkEntrancesListDto;
            }
            else
            {
                
            }
        }

        private async  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var carTypes = await _carTypesAppService.GetTypesListDtos();
            LoadParkArea();
            _entranceViewModel.CarTypesLists = new System.Collections.ObjectModel.ObservableCollection<CarTypeses.Dtos.CarTypesListDto>(carTypes);
            //_entranceViewModel.EntrancesListDtos = new System.Collections.ObjectModel.ObservableCollection<ParkEntranceses.Dtos.ParkEntrancesListDto>(parkEntrance);


            var parkAea = await _parkAreasAppService.GetParkAreaDtos(_mainWindowViewModel.SelectParkDto.Id);

            _entranceViewModel.ParkAreaDtos = new System.Collections.ObjectModel.ObservableCollection<ParkAreases.Dtos.ParkAreaDto>(parkAea);

            //Cmb_Area.DataContext = _entranceViewModel.ParkAreaDtos;
        }

        private async void LoadParkArea()
        {

            var parkAea = await _parkAreasAppService.GetParkAreaDtos(_mainWindowViewModel.SelectParkDto.Id);

            trvFamilies.ItemsSource = parkAea;
            List<ParkEntrancesListDto> list;
            foreach (var item in parkAea)
            {
                list = await _parkEntrancesAppService.GetParkEntrancesListDtosByAreaId(item.Id);
                item.EntrancesListDtos = new System.Collections.ObjectModel.ObservableCollection<ParkEntrancesListDto>(list);
            }

        }

      

        private void Cmb_Types_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Cmb_Types.SelectedItem = null;
        }
    }
}
