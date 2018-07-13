using Abp.Dependency;
using Castle.MicroKernel.Registration;
using MahApps.Metro.Controls;
using Park.CreateCameraPnel;
using Park.Froms;
using Park.ParkBox;
using Park.UserControls;
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

namespace Park.Froms
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : AbpWindow, ISingletonDependency, IManualEntryAndExit
    {
        private readonly IParkBoxOptions parkBoxOptions;
        private readonly ICreatePnel _createPnel;
        public MainWindow(IParkBoxOptions parkBoxOptions, ICreatePnel createPnel)
        {
            InitializeComponent();
            DataContext = this;
            this.parkBoxOptions = parkBoxOptions;
            _createPnel = createPnel;
            UserCard.Children.Add(IocManager.Instance.Resolve<UserCard>());
            IocManager.Instance.IocContainer.Register(
                Component.For<IManualEntryAndExit>().Instance(this)
                );
            _createPnel.CreatePnels(this.ContentCamera);
            
        }

        /// <summary>
        /// 手工出入场
        /// </summary>
        /// <param name="entranceId"></param>
        public void ManualEntryAndExit(long? entranceId)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TiggleFlyout(0);
        }

     


        private bool TiggleFlyout(int index)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return false;
            }

            flyout.IsOpen = !flyout.IsOpen;
            return flyout.IsOpen;
        }

        
    }
}
