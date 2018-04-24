using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Park.Controls
{
    public class MenuViewControl
    {
        private ParkMainControl mainControl;

        private NotifyIcon notifyIcon;

        private Bitmap icon_baseBitmap;
        private ContextMenu contextMenu1;
        private bool _isFirstRun;
        private bool _isStartupChecking;

        private MenuItem menuItem;


        public MenuViewControl(ParkMainControl mainControl)
        {

        }

    }
}
