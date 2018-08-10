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
    /// HiKPicture.xaml 的交互逻辑
    /// </summary>
    public partial class HiKPicture : System.Windows.Controls.UserControl,IHandler
    {
        
        private PictureBox pictureBox;
        public PictureBox PictureBox { get { return pictureBox; } }

        public IntPtr IntPtr => pictureBox == null ? IntPtr.Zero : pictureBox.Handle;

        private DeviceInfoDto deviceInfo;
        private System.Threading.Timer timer;

        private Label _labelName;
        private Label _labelTime;

        private Button _btnInOut;
        private Button _btnOpen;
        private Button _btnClose;


        private Action _openRod;
        private Action _inOutAction;
        private IParkBoxOptions _parkBoxOptions;
        private ParkEntranceInfo _parkEntranceInfo;

        private void InitCoverLayer(Panel panel)
        {
            InitControls();
            pictureBox.BackColor = Color.Transparent;
            panel.Controls.Add(_labelName);
            panel.Controls.Add(_labelTime);
            panel.Controls.Add(_btnClose);
            panel.Controls.Add(_btnInOut);
            panel.Controls.Add(_btnOpen);
            panel.BackColor = Color.Transparent;
            _labelTime.Parent = pictureBox;
            _labelTime.BackColor = Color.Transparent;
            _labelName.Parent = pictureBox;
            _labelName.BackColor = Color.Transparent;
            _labelTime.AutoSize = true;
            _labelName.AutoSize = true;
            pictureBox.SendToBack();
            timer = new System.Threading.Timer(x =>
            {
                this.Dispatcher.Invoke(() => this._labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            }, null, 1000, 1000);


            _labelName.Top = _labelTime.Top = 20;
            _labelName.Left = 50;

        }
        private void InitControls()
        {
            _labelName = new Label();
            _labelTime = new Label();
            
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
                _labelTime.ForeColor= Color.Green;
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
    


        public HiKPicture(DeviceInfoDto deviceInfoDto,Action openRod, Action inOutAction,IParkBoxOptions parkBoxOptions,ParkEntranceInfo parkEntranceInfo)
        {
            InitializeComponent();
            deviceInfo = deviceInfoDto;
            _parkBoxOptions = parkBoxOptions;
            _parkEntranceInfo = parkEntranceInfo;
            //      System.Windows.Forms.Integration.WindowsFormsHost host =
            //new System.Windows.Forms.Integration.WindowsFormsHost();
            _openRod = openRod;
            _inOutAction = inOutAction;
            Panel panel = new Panel();
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox.SendToBack();
            deviceInfoDto.Handler = panel.Handle;
            panel.Controls.Add(pictureBox);
            InitCoverLayer(panel);
            host.Child = panel;
        }

        //public Task SetImage(Stream stream)
        //{
        //    pictureBox.Image = System.Drawing.Image.FromStream(stream);
        //    return Task.CompletedTask;
        //}

        //void IHandler.SetImage(Stream stream)
        //{
        //    pictureBox.Image = System.Drawing.Image.FromStream(stream);
        //}

        private void Content_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this._labelTime.Left = (int)(this.ActualWidth / 2 + 50);

            this._btnInOut.Top = this._btnOpen.Top = this._btnClose.Top = (int)(this.ActualHeight - 20 - 40);


            this._btnInOut.Left = 50;

            this._btnOpen.Left = (int)(this.ActualWidth / 2 + 30);
            this._btnClose.Left = (int)(this.ActualWidth / 2 + 50 + 80);

            pictureBox.Image = System.Drawing.Image.FromStream(_parkBoxOptions.DefultCarmeraImg);
        }

        public void SetImage(Stream stream)
        {
            if (!_parkBoxOptions.IsListView)
                pictureBox.Image = System.Drawing.Image.FromStream(stream);
            else
                _parkEntranceInfo.SetImage(stream);
        }
    }
}
