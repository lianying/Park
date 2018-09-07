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
using System.Windows.Shapes;
using Abp.Dependency;
using Park.CarPorts;
using Park.CarPorts.Dtos;
using Park.CarUserses.Dtos;
using Park.Childers.ViewModels;
using Park.ViewModel;

namespace Park.Childers
{
    /// <summary>
    /// AddCarport.xaml 的交互逻辑
    /// </summary>
    public partial class AddCarport : ITransientDependency
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private AddPortsViewModel _carPortViewModel;
        private readonly ICarPortAppService _carPortAppService;

        private CarUsersListDto _carUsersListDto;
        public AddCarport(MainWindowViewModel mainWindowViewModel, ICarPortAppService carPortAppService)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _carPortViewModel = new AddPortsViewModel();
            this.DataContext = _carPortViewModel;
            _carPortAppService = carPortAppService;
        }


        public AddCarport(MainWindowViewModel mainWindowViewModel,ICarPortAppService carPortAppService,CarUsersListDto carUsersListDto):this(mainWindowViewModel,carPortAppService)
        {
            this._carUsersListDto = carUsersListDto;
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _carPortViewModel.CarPortEditDto.CarPortTypeId = (long)Cmb_Area.SelectedValue;
            _carPortViewModel.CarPortEditDto.AreaId = (long)Cmb_CarType.SelectedValue;
            if (_carUsersListDto != null && _carUsersListDto.Id > 0)
            {
                _carPortViewModel.CarPortEditDto.CarUserId = _carUsersListDto.Id;
            }
            await _carPortAppService.CreateOrUpdateCarPort(new CreateOrUpdateCarPortInput() { CarPort = _carPortViewModel.CarPortEditDto });

            this.DialogResult = true;

            this.Close();
        }
    }
}
