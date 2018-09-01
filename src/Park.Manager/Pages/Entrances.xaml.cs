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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

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
        }

        private async  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var carTypes = await _carTypesAppService.GetTypesListDtos();
            var parkAea = await _parkAreasAppService.GetParkAreaDtos(_mainWindowViewModel.SelectParkDto.Id);
            List<ParkEntrancesListDto> list;
            foreach (var item in parkAea)
            {
                list = await _parkEntrancesAppService.GetParkEntrancesListDtosByAreaId(item.Id);
                item.EntrancesListDtos = new System.Collections.ObjectModel.ObservableCollection<ParkEntrancesListDto>(list);
            }


            trvFamilies.ItemsSource = parkAea;
            _entranceViewModel.CarTypesLists = new System.Collections.ObjectModel.ObservableCollection<CarTypeses.Dtos.CarTypesListDto>(carTypes);
            //_entranceViewModel.EntrancesListDtos = new System.Collections.ObjectModel.ObservableCollection<ParkEntranceses.Dtos.ParkEntrancesListDto>(parkEntrance);
            _entranceViewModel.ParkAreaDtos = new System.Collections.ObjectModel.ObservableCollection<ParkAreases.Dtos.ParkAreaDto>(parkAea);

            
            
        }
    }
}
