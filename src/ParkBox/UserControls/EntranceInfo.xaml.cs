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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Park.UserControls
{
    [ContentProperty(nameof(Children))]
    /// <summary>
    /// EntranceInfo.xaml 的交互逻辑
    /// </summary>
    public partial class EntranceInfo : UserControl
    {
        public static readonly DependencyPropertyKey ChildrenProperty = DependencyProperty.RegisterReadOnly(
            nameof(Children),  // Prior to C# 6.0, replace nameof(Children) with "Children"
            typeof(UIElementCollection),
            typeof(EntranceInfo),
            new PropertyMetadata());

        public UIElementCollection Children {
            get { return (UIElementCollection)GetValue(ChildrenProperty.DependencyProperty); }
            private set { SetValue(ChildrenProperty, value); }
        }

        public string CarNumber { get; set; }

        public string CarType { get; set; }

        public string EntranceName { get; set; }

        public string PayMoney { get; set; }

        public EntranceInfo()
        {
            InitializeComponent();
            DataContext = this;
            Children = grd_content.Children;
        }
    }
}
