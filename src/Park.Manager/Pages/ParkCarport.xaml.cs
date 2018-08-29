using Park.CarPorts;
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
using Park.ParkAreases.Dtos;

namespace Park.Pages
{
    /// <summary>
    /// ParkCarport.xaml 的交互逻辑
    /// </summary>
    public partial class ParkCarport : Page,ISingletonDependency
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly ICarPortAppService _carPortAppService;
        private ParkCarportViewModel _parkCarportViewModel;
        public ParkCarport(MainWindowViewModel mainWindowViewModel, ICarPortAppService carPortAppService)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _carPortAppService = carPortAppService;
            _parkCarportViewModel = new ParkCarportViewModel();
            this.DataContext = _parkCarportViewModel;

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {


            var carTypes = await _carPortAppService.GetCarTypes();
            carTypes.Insert(0, new CarTypeses.Dtos.CarTypesListDto() { Id = 0, CustomName = "请选择" });
            _parkCarportViewModel.CarTypesDto = new System.Collections.ObjectModel.ObservableCollection<CarTypeses.Dtos.CarTypesListDto>(carTypes);
            var parkArea =await _carPortAppService.GetParkAreaDtosByParkId(_mainWindowViewModel.SelectParkDto.Id);

            parkArea.Insert(0, new ParkAreaDto() { Id = 0,  AreaName = "请选择" });
            _parkCarportViewModel.ParkAreaDtos = new System.Collections.ObjectModel.ObservableCollection<ParkAreases.Dtos.ParkAreaDto>(parkArea);

            var gridTable = await _carPortAppService.GetPagedCarPorts(new CarPorts.Dtos.GetCarPortsInput() { SkipCount = 0, MaxResultCount = 100000, ParkId = _mainWindowViewModel.SelectParkDto.Id });
            _parkCarportViewModel.TotleCount = gridTable.TotalCount;
            _parkCarportViewModel.CarPortListDtos = new System.Collections.ObjectModel.ObservableCollection<CarPorts.Dtos.CarPortListDto>(gridTable.Items);

            var carPortCount = await _carPortAppService.GetParkCarportParkingSpaceCountDto(_mainWindowViewModel.SelectParkDto.Id);

            _parkCarportViewModel.RentCount = carPortCount.RentCount;
            _parkCarportViewModel.SellCount = carPortCount.SellCount;
            _parkCarportViewModel.RemainingCount = carPortCount.RemainingCount;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(_parkCarportViewModel.CarportNumber);

            var gridTable = await _carPortAppService.GetPagedCarPorts(new CarPorts.Dtos.GetCarPortsInput() { SkipCount = 0, MaxResultCount = 100000, ParkId = _mainWindowViewModel.SelectParkDto.Id, Filter = _parkCarportViewModel.CarportNumber, AreaId = (long)Cmb_Area.SelectedValue, CarTypeId = (int)Cmb_CarType.SelectedValue });

            _parkCarportViewModel.CarPortListDtos = new System.Collections.ObjectModel.ObservableCollection<CarPorts.Dtos.CarPortListDto>(gridTable.Items);
        }
    }
}
