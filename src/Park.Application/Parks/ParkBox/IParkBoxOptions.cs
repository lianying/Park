using Park.Parks.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Interfaces;
using Park.Devices.Models;
using Abp.Dependency;
using System.IO;
using Park.Users.Dto;
using Park.Authorization.Users;
using Park.Parks.ParkBox.Interfaces;

namespace Park.ParkBox
{
    public interface IParkBoxOptions: ISingletonDependency
    {
        /// <summary>
        /// 车场名
        /// </summary>
        string ParkName { get; set; }

        /// <summary>
        /// 车场编号
        /// </summary>
        int ParkId { get; set; }
        /// <summary>
        /// 本机IP列表
        /// </summary>

        List<string> LocalIps { get; set; }


        /// <summary>
        /// 是否展示实时画面
        /// </summary>
        bool IsListView { get; set; }

        /// <summary>
        /// 实时画面个数
        /// </summary>
        ListViewEnum ListViewCount { get; set; }

        /// <summary>
        /// 双路协同时 等待第二个回调结果的时间
        /// </summary>
        TimeSpan DoubleVideoWaitingTimes { get; set; }

        /// <summary>
        /// 图片服务地址
        /// </summary>
        string ImgServiceAddress { get; set; }
        /// <summary>
        /// 图片服务登录用户名
        /// </summary>
        string ServiceUserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        string ServicePwd { get; set; }

        /// <summary>
        /// 图片保留天数
        /// </summary>
        int DeletePhotoDays { get; set; }

        /// <summary>
        /// 管理中心地址
        /// </summary>
        string ServiceIp { get; set; }


        /// <summary>
        /// 绑定设备
        /// </summary>
        IReadOnlyList<DeviceInfoDto> DeciceInfos { get; set; }

        /// <summary>
        /// 设备重连时间
        /// </summary>
        TimeSpan ReLoginTime { get; set; }

        /// <summary>
        /// 过滤器
        /// </summary>
        IReadOnlyList<IFilterable> Filters { get; }


        bool NonmotorVehicleIn { get; set; }


        Stream UserImg { get; set; }


        User User { get; set; }


        Stream DefultUserImg { get; }


        Stream DefultCarmeraImg { get; }



        long TempCarTypeId { get; set; }


        bool IsZeroMoneyOut { get; set; }

        Dictionary<long, ISetInfo> SetInfosDic { get; set; }
        
    }
}
