using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Castle.Core.Logging;
using MahApps.Metro.Controls;
using Park.ParkBox;
using Park.Parks.Entrance;
using Park.UserControls;

namespace Park.CreateCameraPnel
{
    public class CreatePnel : ICreatePnel
    {
        private readonly IParkBoxOptions _parkBoxOptions;
        private int countEntrances = 0;
        private IEnumerable<IGrouping<long, Devices.Models.DeviceInfoDto>> deviceInfoDtos;

        public ILogger Logger { get; set; }
        public CreatePnel(IParkBoxOptions parkBoxOptions)
        {

            _parkBoxOptions = parkBoxOptions;
            deviceInfoDtos = _parkBoxOptions.DeciceInfos.GroupBy(x => x.EntranceDto.Id);
            countEntrances = deviceInfoDtos.Count();
            Logger = NullLogger.Instance;
        }
        /// <summary>
        /// 最多只展示4个画面
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public Dictionary<long, ParkEntranceInfo> CreatePnels(Control window)
        {
            if (countEntrances == 0)
                return null;
            FlipView flipView = window as FlipView;
            Dictionary<long, ParkEntranceInfo> dic = new Dictionary<long, ParkEntranceInfo>();
            if (countEntrances <= 2)
            {
                FlipViewItem flipViewItem = new FlipViewItem();
                Grid grid = new Grid();
                flipViewItem.Content = grid;
                if (countEntrances == 1)  //一个出入口
                {
                    var entrance = new ParkEntranceInfo(_parkBoxOptions.DeciceInfos.FirstOrDefault(), _parkBoxOptions, Logger);
                    dic.Add(deviceInfoDtos.First().Key, entrance);
                    grid.Children.Add(entrance);
                   
                    entrance.Init();
                }
                else   //两个出入口
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    ParkEntranceInfo parkEntrance;
                    int i = 0;
                    foreach (var item in deviceInfoDtos)
                    {
                        parkEntrance = new ParkEntranceInfo(item.First(), _parkBoxOptions, Logger);
                        grid.Children.Add(parkEntrance);
                        Grid.SetColumn(parkEntrance, i);
                        parkEntrance.Init();
                        i++;
                    }
                }
                flipView.Items.Add(flipViewItem);


            }
            else
            {

            }


            return dic;
        }


    }
}
