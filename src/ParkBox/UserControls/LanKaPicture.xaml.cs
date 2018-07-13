using System;
using System.Collections.Generic;
using System.IO;
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
using AxBLC_IPNCLib;

namespace Park.UserControls
{
    /// <summary>
    /// LanKaPicture.xaml 的交互逻辑
    /// </summary>
    public partial class LanKaPicture : UserControl,IHandler
    {
        private AxBLC_IPNC _lankaPic;
        public AxBLC_IPNC LankaPic { get { return _lankaPic; } }
        public LanKaPicture()
        {
            InitializeComponent();
            System.Windows.Forms.Integration.WindowsFormsHost host =
    new System.Windows.Forms.Integration.WindowsFormsHost();
            _lankaPic = new AxBLC_IPNC();

            host.Child = LankaPic;
            this.Content.Children.Add(host);
        }

        public IntPtr IntPtr => _lankaPic != null ? _lankaPic.Handle : IntPtr.Zero;

        /// <summary>
        /// 实时监控控件 不予设置图片
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Task SetImage(Stream stream)
        {
            return Task.CompletedTask;
        }
    }
}
