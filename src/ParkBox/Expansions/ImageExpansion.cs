using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Park.Expansions
{
    public static class ImageExpansion
    {
        public static void SetImage(this System.Windows.Controls.Image image,Stream stream) {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            image.Source = bitmapImage;
        }
    }
}
