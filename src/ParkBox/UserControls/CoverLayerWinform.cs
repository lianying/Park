using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Park.Devices.Models;
using Park.ParkBox;
using Castle.Core.Logging;
using System.IO;

namespace Park.UserControls
{
    public partial class CoverLayerWinform : UserControl,IHandler
    {

        private CoverLayer _coverLayer;

        ElementHost elementHost = new ElementHost();
        public CoverLayerWinform(DeviceInfoDto deviceInfoDto, IParkBoxOptions parkBoxOptions, ILogger logger, Action openRod, Action inOutAction)
        {
            InitializeComponent();
            _coverLayer = new CoverLayer(deviceInfoDto, parkBoxOptions, logger, openRod, inOutAction);
            _coverLayer.Background = System.Windows.Media.Brushes.Transparent;
            
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = _coverLayer;
            panel1.Controls.Add(elementHost);
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.SendToBack();
            //elementHost.Refresh();
        }

        //protected override void OnLoad(EventArgs e)
        //{

        //    elementHost.BackColorTransparent = true;
        //    //elementHost.BackColor = Color.Transparent;
        //}

        public IntPtr IntPtr => pictureBox1.Handle;

        public void SetImage(Stream stream)
        {
            pictureBox1.BackgroundImage = Image.FromStream(stream);
        }

        private void CoverLayerWinform_Load(object sender, EventArgs e)
        {

            elementHost.BackColorTransparent = true;
        }
    }
}
