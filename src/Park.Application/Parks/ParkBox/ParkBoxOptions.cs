using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Interfaces;
using Park.Parks.Devices;

namespace Park.ParkBox
{
    public class ParkBoxOptions : IParkBoxOptions
    {
        public bool IsListView { get ; set ; }
        public ListViewEnum ListViewCount { get ; set ; }
        public TimeSpan DoubleVideoWaitingTimes { get; set; }
        public string ImgServiceAddress { get ; set ; }
        public string ServiceUserName { get ; set ; }
        public string ServicePwd { get; set ; }
        public int DeletePhotoDays { get ; set ; }
        public string ServiceIp { get ; set ; }
        public IReadOnlyList<DeviceInfoDto> DeciceInfos { get ; set ; }
        public TimeSpan ReLoginTime { get ; set; }

        public IReadOnlyList<IFilterable> Filters => throw new NotImplementedException();
    }
}
