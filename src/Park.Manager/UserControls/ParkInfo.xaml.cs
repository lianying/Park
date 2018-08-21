using Park.ParkBox.Dto;
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
    /// ParkInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ParkInfo : UserControl
    {

        static Random r;

        static ParkInfo()
        {
            r = new Random();
        }
        int index = 0;
        public ParkInfo()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var context=this.DataContext as ParkDto;
            index = r.Next(0, ParkConsts.ParkHeadColor.Length);
            var color = ParkConsts.ParkHeadColor[index];
            var bc = new BrushConverter();
            context.HeadColor = color;
            Top.Background = (Brush)bc.ConvertFrom(color);
        }
    }
}
