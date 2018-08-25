using Park.Parks.Park;
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
using Abp.ObjectMapping;
using Park.Parks.Park.Dto;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Abp.AutoMapper;

namespace Park.Pages
{
    /// <summary>
    /// ParkInfo.xaml 的交互逻辑
    /// </summary>
    public partial class ParkInfo : Page,ITransientDependency
    {
        private readonly IParkAppService _parkAppService;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IObjectMapper objectMapper;
        public ParkInfo(IParkAppService parkAppService, MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _parkAppService = parkAppService;
            _mainWindowViewModel = mainWindowViewModel;
            this.DataContext = mainWindowViewModel.SelectParkDto;
            objectMapper = NullObjectMapper.Instance;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            var main = Application.Current.MainWindow as MetroWindow;
            try
            {
                //   var park = objectMapper.Map<CreateParkDto>();
                var park = _mainWindowViewModel.SelectParkDto.MapTo<CreateParkDto>();
                await _parkAppService.Update(park);
                await main.ShowMessageAsync("提示", "保存成功");

            }
            catch(Exception ex)
            {
                await main.ShowMessageAsync("提示", "保存失败");
            }

        }
    }
}
