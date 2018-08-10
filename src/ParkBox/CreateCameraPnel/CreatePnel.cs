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
using Park.Parks.ParkBox.Interfaces;
using Park.UserControls;

namespace Park.CreateCameraPnel
{
    public class CreatePnel : ICreatePnel
    {
        private readonly IParkBoxOptions _parkBoxOptions;
        private int countEntrances = 0;
        private List<IGrouping<long, Devices.Models.DeviceInfoDto>> deviceInfoDtos;

        public ILogger Logger { get; set; }
        public CreatePnel(IParkBoxOptions parkBoxOptions)
        {

            _parkBoxOptions = parkBoxOptions;
            deviceInfoDtos = _parkBoxOptions.DeciceInfos?.GroupBy(x => x.EntranceDto.Id).ToList();
            countEntrances = deviceInfoDtos?.Count() ?? 0;
            Logger = NullLogger.Instance;
        }
        /// <summary>
        /// 最多只展示4个画面
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public Dictionary<long, ISetInfo> CreatePnels(Control window)
        {
            if (countEntrances == 0)
                return null;
            FlipView flipView = window as FlipView;
            Dictionary<long, ISetInfo> dic = new Dictionary<long, ISetInfo>();
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
                    //if (_parkBoxOptions.IsListView)
                    //{
                    //    entrance.Loaded += delegate
                    //    {
                    //        entrance.ReSizePic();
                    //    };
                    //}
                }
                else   //两个出入口
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    ParkEntranceInfo parkEntrance;
                    int i = 0;
                    foreach (var item in deviceInfoDtos)
                    {

                        if (!dic.Keys.Contains(item.Key))
                        {
                            parkEntrance = new ParkEntranceInfo(item.First(), _parkBoxOptions, Logger);
                            grid.Children.Add(parkEntrance);
                            Grid.SetColumn(parkEntrance, i);
                            dic.Add(item.Key, parkEntrance);
                            parkEntrance.Init();
                            //if (_parkBoxOptions.IsListView)
                            //{
                            //    parkEntrance.Loaded += delegate
                            //    {
                            //        parkEntrance.ReSizePic();
                            //    };
                            //}
                            i++;
                        }
                    }
                }
                flipView.Items.Add(flipViewItem);


            }
            else
            {
                FlipViewItem flipViewItem;
                Grid grid;
                int o = 0;
                ParkEntranceInfo parkEntrance;
                var count = countEntrances / 4 + ((countEntrances % 4) > 0 ? 1 : 0);
                for (int i = 0; i < count; i++)
                {
                    flipViewItem = new FlipViewItem();
                    grid = new Grid();
                    //2x2
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition());

                    //foreach (var item in deviceInfoDtos)
                    //{
                    //    if (!dic.Keys.Contains(item.Key))
                    //    {
                    //        parkEntrance = new ParkEntranceInfo(item.First(), _parkBoxOptions, Logger);
                    //        parkEntrance.Margin = new System.Windows.Thickness() { Bottom = 5, Left = 5, Right = 5, Top = 5 };
                    //        grid.Children.Add(parkEntrance);

                    //        if (i == 1)
                    //        {
                    //            Grid.SetColumn(parkEntrance, 1);
                    //        }
                    //        else if (i == 2)
                    //        {
                    //            Grid.SetRow(parkEntrance, 1);
                    //        }
                    //        else if (i == 3)
                    //        {
                    //            Grid.SetRow(parkEntrance, 1);
                    //            Grid.SetColumn(parkEntrance, 1);
                    //        }
                    //        dic.Add(item.Key, parkEntrance);
                    //        parkEntrance.Init();

                    //        i++;
                    //    }
                    //}

                    for (; o < countEntrances; o++)
                    {
                        if (!dic.Keys.Contains(deviceInfoDtos[o].Key))
                        {
                            parkEntrance = new ParkEntranceInfo(deviceInfoDtos[o].First(), _parkBoxOptions, Logger);
                            parkEntrance.Margin = new System.Windows.Thickness() { Bottom = 5, Left = 0, Right = 5, Top = 5 };
                            grid.Children.Add(parkEntrance);
                            //if (_parkBoxOptions.IsListView)
                            //{
                            //    parkEntrance.Loaded += delegate
                            //    {
                            //        parkEntrance.ReSizePic();
                            //    };
                            //}
                            if (o == 1)
                            {
                                Grid.SetColumn(parkEntrance, 1);
                            }
                            else if (o == 2)
                            {
                                Grid.SetRow(parkEntrance, 1);
                            }
                            else if (o == 3)
                            {
                                Grid.SetRow(parkEntrance, 1);
                                Grid.SetColumn(parkEntrance, 1);
                                dic.Add(deviceInfoDtos[o].Key, parkEntrance);
                                parkEntrance.Init();
                                break;
                            }
                            dic.Add(deviceInfoDtos[o].Key, parkEntrance);
                            parkEntrance.Init();
                            
                        }
                    }
                    flipViewItem.Content = grid;
                    flipView.Items.Add(flipViewItem);
                }
            }


            return dic;
        }


    }
}
