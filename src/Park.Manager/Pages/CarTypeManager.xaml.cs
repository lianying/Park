using Abp.Dependency;
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

namespace Park.Pages
{
    /// <summary>
    /// CarTypeManager.xaml 的交互逻辑
    /// </summary>
    public partial class CarTypeManager : Page, ISingletonDependency
    {
        public CarTypeManager()
        {
            InitializeComponent();
        }

        private void trvFamilies_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
