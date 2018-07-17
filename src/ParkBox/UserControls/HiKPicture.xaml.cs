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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Park.UserControls
{
    /// <summary>
    /// HiKPicture.xaml 的交互逻辑
    /// </summary>
    public partial class HiKPicture : System.Windows.Controls.UserControl,IHandler
    {
        private PictureBox pictureBox;
        public PictureBox PictureBox { get { return pictureBox; } }

        public IntPtr IntPtr => pictureBox == null ? IntPtr.Zero : pictureBox.Handle;

        public HiKPicture()
        {
            InitializeComponent();
            System.Windows.Forms.Integration.WindowsFormsHost host =
      new System.Windows.Forms.Integration.WindowsFormsHost();

            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            host.Child = pictureBox;
            this.Content.Children.Add(host);
        }

        public Task SetImage(Stream stream)
        {
            pictureBox.Image = System.Drawing.Image.FromStream(stream);
            return Task.CompletedTask;
        }
    }
}
