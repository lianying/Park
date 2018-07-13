using Park.Entitys.CarUsers;
using Park.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.ParkBox.Models
{
    public class PermissionResult
    {
        public PermissionResult (bool? isCarIn,CarNumberPermissionEnum carNumberPermissionEnum, CarUsers carUsers,bool IsMonthTempIn)
        {

            this.IsCarIn = IsCarIn;
            this.CarNumberPermission = carNumberPermissionEnum;
            this.CarUser = carUsers;
            this.IsMonthTempIn = IsMonthTempIn;
        }

        /// <summary>
        /// 是否允许进入  为null时弹窗提示
        /// </summary>
        public bool? IsCarIn { get; private set; }

        public CarNumberPermissionEnum CarNumberPermission { get;private set; }


        public CarUsers CarUser { get; private set; }


        public bool IsMonthTempIn { get;private set; }




        public string Message
        {
            get
            {
                switch (CarNumberPermission)
                {
                    case CarNumberPermissionEnum.TempConfimIn:
                        return ParkConsts.TempCarInConfirmMessage;
                    case CarNumberPermissionEnum.NoPermissionNotIn:
                        return ParkConsts.NoPermissionNotInMessage;
                    case CarNumberPermissionEnum.NoNumberNotIn:
                        return ParkConsts.NoNumberNotInMessage;
                    case CarNumberPermissionEnum.CarportsFullNotIn:
                        return ParkConsts.CarportsFullNotInMessage;
                    case CarNumberPermissionEnum.TempNotIn:
                        return ParkConsts.TempCarNotInMessage;
                    case CarNumberPermissionEnum.NoNumberConfimIn:
                        return ParkConsts.NoNumberCarInConfirmMessage;
                    default:
                        return string.Empty;
                }
            }
        }

        public override string ToString()
        {
            return $"{CarNumberPermission.ToString()}";
        }



    }
}
