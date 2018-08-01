using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Collections.ObjectModel;

namespace Park.UserControls
{
    /// <summary>
    /// PageUC.xaml 的交互逻辑
    /// </summary>
    public partial class PageUC : UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        public PageUC()
        {
            InitializeComponent();
            GoPage = new ObservableCollection<WinPage>();
            this.DataContext = this;
        }
        private int pageDataCount = 10;
        //每页多少条
        public int PageDataCount
        {
            get { return pageDataCount; }
            set { if (value <= 0) { value = 10; } pageDataCount = value; }
        }

        //private int dataCount;
        ////总条数
        //public int DataCount
        //{
        //    get { return dataCount; }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            value = 0;
        //        }
        //        if (value == 0)
        //            PageCount = 1;
        //        else
        //            PageCount = (value % PageDataCount) > 0 ? value / PageDataCount + 1 : value / PageDataCount;
        //        dataCount = value;
        //    }
        //}

        public int DataCount
        {
            get { return (int)GetValue(DataCountProperty); }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                if (value == 0)
                    PageCount = 1;
                else
                    PageCount = (value % PageDataCount) > 0 ? value / PageDataCount + 1 : value / PageDataCount;
                SetValue(DataCountProperty, value);
            }
        }
        public static readonly DependencyProperty DataCountProperty = DependencyProperty.Register("DataCount", typeof(int), typeof(PageUC), new PropertyMetadata(0));



        private int pageCount;
        //总页数
        public int PageCount { get { return pageCount; } set { pageCount = value; } }

        //private int pageIndex = 1;
        ////当前第几页
        //public int PageIndex
        //{
        //    get
        //    {
        //        return pageIndex;
        //    }
        //    set
        //    {
        //        if (value <= 0)
        //        {
        //            value = 1;
        //        }
        //        if (value > PageCount)
        //        {
        //            value = PageCount;
        //        }
        //        pageIndex = value;
        //        SumPageCount();
        //    }
        //}

        //当前第几页
        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set
            {
                if (value <= 0)
                {
                    value = 1;
                }
                if (value > PageCount)
                {
                    value = PageCount;
                }
                SetValue(PageIndexProperty, value);
                SumPageCount();
            }
        }

        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(PageUC), new PropertyMetadata(1));


        public event EventHandler BtnDown;

        private ObservableCollection<WinPage> goPage;
        //当前界面页按钮
        public ObservableCollection<WinPage> GoPage
        {
            get { return goPage; }
            set { goPage = value; NotifyChanged("GoPage"); }
        }

        public class WinPage
        {
            public int Index { get; set; }
            public bool Bg { get; set; }
        }

        #region 上页、下页、首页、末页（事件）
        private void btn_UpPage(object sender, RoutedEventArgs e)
        {
            PageIndex--;
        }
        private void btn_DownPage(object sender, RoutedEventArgs e)
        {
            PageIndex++;
        }
        #endregion


        //根据当前数据处理界面按钮显示隐藏和是否可用
        void SumPageCount()
        {
            if (DataCount <= 0)
            {
                GoPage.Clear();
                Txt_PointStart.Visibility = Visibility.Collapsed;
                Txt_PointEnd.Visibility = Visibility.Collapsed;
                BtnFirstPage.Visibility = Visibility.Collapsed;
                BtnEndPage.Visibility = Visibility.Collapsed;
            }
            if (PageCount <= 1)
            {
                GoPage.Clear();
                GoPage.Add(new WinPage() { Index = 1 });
                Txt_PointStart.Visibility = Visibility.Collapsed;
                Txt_PointEnd.Visibility = Visibility.Collapsed;
                BtnFirstPage.Visibility = Visibility.Collapsed;
                BtnEndPage.Visibility = Visibility.Collapsed;
            }
            else if (PageCount == 2)
            {
                GoPage.Clear();
                GoPage.Add(new WinPage() { Index = 1 });
                GoPage.Add(new WinPage() { Index = 2 });
                Txt_PointStart.Visibility = Visibility.Collapsed;
                Txt_PointEnd.Visibility = Visibility.Collapsed;
                BtnFirstPage.Visibility = Visibility.Collapsed;
                BtnEndPage.Visibility = Visibility.Collapsed;
            }
            else if (PageCount == 3)
            {
                GoPage.Clear();
                GoPage.Add(new WinPage() { Index = 1 });
                GoPage.Add(new WinPage() { Index = 2 });
                GoPage.Add(new WinPage() { Index = 3 });
                Txt_PointStart.Visibility = Visibility.Collapsed;
                Txt_PointEnd.Visibility = Visibility.Collapsed;
                BtnFirstPage.Visibility = Visibility.Collapsed;
                BtnEndPage.Visibility = Visibility.Collapsed;
            }
            else if (PageCount == 4)
            {
                GoPage.Clear();
                GoPage.Add(new WinPage() { Index = 1 });
                GoPage.Add(new WinPage() { Index = 2 });
                GoPage.Add(new WinPage() { Index = 3 });
                GoPage.Add(new WinPage() { Index = 4 });
                Txt_PointStart.Visibility = Visibility.Collapsed;
                Txt_PointEnd.Visibility = Visibility.Collapsed;
                BtnFirstPage.Visibility = Visibility.Collapsed;
                BtnEndPage.Visibility = Visibility.Collapsed;
            }
            else if (PageCount == 5)
            {
                GoPage.Clear();
                GoPage.Add(new WinPage() { Index = 1 });
                GoPage.Add(new WinPage() { Index = 2 });
                GoPage.Add(new WinPage() { Index = 3 });
                GoPage.Add(new WinPage() { Index = 4 });
                GoPage.Add(new WinPage() { Index = 5 });
                Txt_PointStart.Visibility = Visibility.Collapsed;
                Txt_PointEnd.Visibility = Visibility.Collapsed;
                BtnFirstPage.Visibility = Visibility.Collapsed;
                BtnEndPage.Visibility = Visibility.Collapsed;
            }
            else if (PageCount >= 6)
            {
                if ((PageIndex - 3) <= 0)
                {
                    Txt_PointStart.Visibility = Visibility.Collapsed;
                    Txt_PointEnd.Visibility = Visibility.Visible;
                    BtnFirstPage.Visibility = Visibility.Collapsed;
                    BtnEndPage.Content = PageCount;
                    BtnEndPage.Visibility = Visibility.Visible;
                    GoPage.Clear();
                    GoPage.Add(new WinPage() { Index = 1 });
                    GoPage.Add(new WinPage() { Index = 2 });
                    GoPage.Add(new WinPage() { Index = 3 });
                    GoPage.Add(new WinPage() { Index = 4 });
                    GoPage.Add(new WinPage() { Index = 5 });
                }
                else if ((PageIndex - 3) > 0 && (PageIndex + 2) < PageCount)
                {
                    if ((PageIndex - 3) > 1)
                        Txt_PointStart.Visibility = Visibility.Visible;
                    else
                        Txt_PointStart.Visibility = Visibility.Collapsed;
                    if ((PageIndex + 3) < PageCount)
                        Txt_PointEnd.Visibility = Visibility.Visible;
                    else
                        Txt_PointEnd.Visibility = Visibility.Collapsed;
                    GoPage.Clear();
                    BtnFirstPage.Visibility = Visibility.Visible;
                    GoPage.Add(new WinPage() { Index = PageIndex - 2 });
                    GoPage.Add(new WinPage() { Index = PageIndex - 1 });
                    GoPage.Add(new WinPage() { Index = PageIndex });
                    GoPage.Add(new WinPage() { Index = PageIndex + 1 });
                    GoPage.Add(new WinPage() { Index = PageIndex + 2 });
                    BtnEndPage.Content = PageCount;
                    BtnEndPage.Visibility = Visibility.Visible;
                }
                else if ((PageIndex + 2) >= PageCount)
                {
                    GoPage.Clear();
                    GoPage.Add(new WinPage() { Index = PageCount - 4 });
                    GoPage.Add(new WinPage() { Index = PageCount - 3 });
                    GoPage.Add(new WinPage() { Index = PageCount - 2 });
                    GoPage.Add(new WinPage() { Index = PageCount - 1 });
                    GoPage.Add(new WinPage() { Index = PageCount });
                    Txt_PointStart.Visibility = Visibility.Visible;
                    Txt_PointEnd.Visibility = Visibility.Collapsed;
                    BtnFirstPage.Visibility = Visibility.Visible;
                    BtnEndPage.Visibility = Visibility.Collapsed;
                }
            }
            if (PageIndex <= 1)
            {
                BtnUpPage.IsEnabled = false;
            }
            else
            {
                BtnUpPage.IsEnabled = true;
            }
            if (PageIndex >= PageCount)
            {
                BtnDownPage.IsEnabled = false;
            }
            else
            {
                BtnDownPage.IsEnabled = true;
            }
            if (DataCount <= 0)
            {
                BtnUpPage.IsEnabled = false;
                BtnUpPage.IsEnabled = false;
            }
            GoPage.First(s => s.Index == PageIndex).Bg = true;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            BtnDown((sender as Button).Content, null);
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            BtnDown((sender as RadioButton).Content, null);
        }

    }
}
