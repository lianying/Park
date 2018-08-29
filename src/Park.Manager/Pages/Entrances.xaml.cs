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
using Park.ParkEntranceses;
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
        public Entrances(MainWindowViewModel mainWindowViewModel, IParkEntrancesAppService parkEntrancesAppService,ICarTypesAppService carTypesAppService)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _parkEntrancesAppService = parkEntrancesAppService;
            _carTypesAppService = carTypesAppService;
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

        }

        private void trvFamilies_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private async  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var carTypes = await _carTypesAppService.GetTypesListDtos();
        }
    }
}
