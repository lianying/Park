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
using Park.CarNumberses.Dtos;
using Park.ViewModel;

namespace Park.UserControls
{
    /// <summary>
    /// CarNumberInfo.xaml 的交互逻辑
    /// </summary>
    public partial class CarNumberInfo : UserControl
    {
        private CarNumbersListDto _carNumber;
        
        public RoutedEventHandler DeleteClickEventHandler;
        public CarNumberInfo(CarNumbersListDto carNumber )
        {
            InitializeComponent();
            _carNumber = carNumber;
            this.DataContext = _carNumber;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            DeleteClickEventHandler?.Invoke(_carNumber, e);
        }
    }
}
