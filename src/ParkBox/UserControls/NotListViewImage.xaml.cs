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

namespace Park.UserControls
{
    /// <summary>
    /// NotListViewImage.xaml 的交互逻辑
    /// </summary>
    public partial class NotListViewImage : Image,IHandler
    {
        public NotListViewImage()
        {
            //InitializeComponent();
        }

        public IntPtr IntPtr => IntPtr.Zero;

        public Task SetImage(Stream stream)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            base.Source = bitmapImage;
            return Task.CompletedTask;
        }

        public void SetWidthAndHeight(double width, double height)
        {
            throw new NotImplementedException();
        }

        void IHandler.SetImage(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
