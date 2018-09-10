using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Extensions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Park.CarTypeses;
using Park.EnumHelper;
using Park.ParkAreases;
using Park.ParkEntranceses;
using Park.ParkEntranceses.Dtos;
using Park.ViewModel;

namespace Park.Pages
{
    /// <summary>
    /// Entrances.xaml 的交互逻辑
    /// </summary>
    public partial class BackLisReportt : Page,ISingletonDependency
    {
        public BackLisReportt()
        {
            InitializeComponent();
        }
    }
}
