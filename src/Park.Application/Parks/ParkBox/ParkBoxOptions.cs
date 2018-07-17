using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Authorization.Users;
using Park.Devices.Models;
using Park.Expansions;
using Park.Interfaces;
using Park.Users.Dto;

namespace Park.ParkBox
{
    public class ParkBoxOptions : IParkBoxOptions
    {
        private Stream defultUserImg;

        private Stream defultCameraImg;
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

        public bool NonmotorVehicleIn { get; set; }
        public Stream UserImg { get ; set ; }
        public User User { get ; set ; }

        public Stream DefultUserImg
        {
            get
            {
                if (defultUserImg == null)
                {
                    defultUserImg = Image.FromFile("DefultUserImg.png").ToStream(ImageFormat.Png);
                }
                return defultUserImg;
            }
        }

        public Stream DefultCarmeraImg
        {
            get {
                if (defultCameraImg == null) {
                    defultCameraImg = Image.FromFile("DefultCameraImg.png").ToStream(ImageFormat.Png);
                }
                return defultCameraImg;
            }
        }


        
    }
}
