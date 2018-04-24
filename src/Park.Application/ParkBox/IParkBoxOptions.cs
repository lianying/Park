﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ParkBox
{
    public interface IParkBoxOptions
    {

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

        

        
    }
}
