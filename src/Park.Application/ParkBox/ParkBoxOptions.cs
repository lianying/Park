using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ParkBox
{
    public class ParkBoxOptions : IParkBoxOptions
    {
        public bool IsListView { get ; set ; }
        public ListViewEnum ListViewCount { get ; set ; }
        public TimeSpan DoubleVideoWaitingTimes { get; set; }
        public string ImgServiceAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ServiceUserName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ServicePwd { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int DeletePhotoDays { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ServiceIp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
