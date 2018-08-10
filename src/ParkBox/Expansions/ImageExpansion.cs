using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Park.Expansions
{
    public static class ImageExpansion
    {

        
        public static void SetImage(this System.Windows.Controls.Image image,Stream stream) {
            stream.Seek(0, SeekOrigin.Begin);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
                image.Source = bitmapImage;
        }
    }
}
