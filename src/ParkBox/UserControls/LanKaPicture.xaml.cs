
using AxBLC_IPNCLib;
using Castle.Core.Logging;
using Park.Devices.Models;
using Park.ParkBox;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Park.UserControls
{
    /// <summary>
    /// LanKaPicture.xaml 的交互逻辑
    /// </summary>
    public partial class LanKaPicture : System.Windows.Controls.UserControl, IHandler
    {
        private AxBLC_IPNC _lankaPic;
        public AxBLC_IPNC LankaPic { get { return _lankaPic; } }


        private DeviceInfoDto deviceInfo;
        private System.Threading.Timer timer;

        private TransparentLabel _labelName;
        private TransparentLabel _labelTime;

        private Button _btnInOut;
        private Button _btnOpen;
        private Button _btnClose;


        private Action _openRod;
        private Action _inOutAction;

        private ParkEntranceInfo _parkEntrance;

        private void InitCoverLayer(Panel panel)
        {
            InitControls();

            panel.Controls.Add(_labelName);
            panel.Controls.Add(_labelTime);
            panel.Controls.Add(_btnClose);
            panel.Controls.Add(_btnInOut);
            panel.Controls.Add(_btnOpen);
            panel.BackColor = Color.Transparent;
            _labelTime.Parent = panel;
            //_labelTime.BackColor = Color.Transparent;
            _labelName.Parent = panel;
            //_labelName.BackColor = Color.Transparent;
            _labelTime.AutoSize = true;
            _labelName.AutoSize = true;
            _lankaPic.SendToBack();
            timer = new System.Threading.Timer(x =>
            {
                this.Dispatcher.Invoke(() => this._labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            }, null, 1000, 1000);


            _labelName.Top = _labelTime.Top = 20;
            _labelName.Left = 50;

        }

        private Button CreateButton()
        {
            Button btn = new Button();
            btn.Width = 100;
            btn.Height = 40;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.Red;
            btn.TabStop = false;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }
        private void InitControls()
        {
            _labelName = new TransparentLabel();
            _labelTime = new TransparentLabel();

            _labelName.Text = deviceInfo.EntranceDto.EntranceName;
            _btnInOut = CreateButton();
            _btnInOut.Click += _btnInOut_Click;
            _btnOpen = CreateButton();
            _btnOpen.Click += _btnOpen_Click;
            _btnOpen.Width = 80;
            _btnOpen.Text = "开闸";
            _btnOpen.BackColor = Color.Green;
            _btnClose = CreateButton();


            _btnClose.Width = 80;
            _btnClose.BackColor = Color.Orange;
            _btnClose.Text = "落闸";
            if (deviceInfo.EntranceDto.EntranceType == Enum.EntranceType.In)
            {
                _labelTime.ForeColor = Color.Orange;
                _labelName.ForeColor = Color.Orange;
                _btnInOut.Text = "手工入场";
            }
            else
            {
                _labelTime.ForeColor = Color.Green;
                _labelName.ForeColor = Color.Green;
                _btnInOut.Text = "手工出场";
            }
        }

        private void _btnOpen_Click(object sender, EventArgs e)
        {
            _openRod?.Invoke();
        }

        private void _btnInOut_Click(object sender, EventArgs e)
        {
            _inOutAction?.Invoke();
        }

        public void SetImage(Stream stream)
        {
            _parkEntrance.SetImage(stream);
        }

        public LanKaPicture(DeviceInfoDto deviceInfoDto, Action openRod, Action inOutAction, ParkEntranceInfo parkEntrance)
        {
            InitializeComponent();
            deviceInfo = deviceInfoDto;
            _parkEntrance = parkEntrance;
            _openRod = openRod;
            _inOutAction = inOutAction;
            Panel panel = new Panel();
            _lankaPic = new AxBLC_IPNC();
            _lankaPic.Dock = DockStyle.Fill;
            _lankaPic.BackgroundImageLayout = ImageLayout.Stretch;
            _lankaPic.SendToBack();
            deviceInfoDto.Handler = panel.Handle;
            panel.Controls.Add(_lankaPic);
            InitCoverLayer(panel);
            host.Child = panel;
        }

        public IntPtr IntPtr => _lankaPic != null ? _lankaPic.Handle : IntPtr.Zero;

        private void Content_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this._labelTime.Left = (int)(this.ActualWidth / 2 + 50);

            this._btnInOut.Top = this._btnOpen.Top = this._btnClose.Top = (int)(this.ActualHeight - 20 - 40);


            this._btnInOut.Left = 50;

            this._btnOpen.Left = (int)(this.ActualWidth / 2 + 30);
            this._btnClose.Left = (int)(this.ActualWidth / 2 + 50 + 80);
        }

        /// <summary>
        /// 实时监控控件 不予设置图片
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        //public Task SetImage(Stream stream)
        //{
        //    return Task.CompletedTask;
        //}
    }
}
