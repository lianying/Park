

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Park.Entitys.ParkEntrances;
using Abp.Domain.Entities;
using Park.CarTypeses.Dtos;
using Park.Enum;

namespace Park.ParkEntrancePermissions.Dtos
{
    public class ParkEntrancePermissionListDto : NotifyPropertyChangeBase<long>, IMayHaveTenant
    {
        private bool _isTempCarIn;
        private bool _isTempCarConfirm;
        private bool _isTempCarZeoPayOut;
        private bool _isNonVehicleIn;
        private NoNumberOptions _noNumberOptions;

        /// <summary>
        /// TenantId
        /// </summary>
        public int? TenantId { get; set; }


        /// <summary>
        /// CarTypes
        /// </summary>
        public string CarTypes { get; set; }


        /// <summary>
        /// IsTempCarIn
        /// </summary>
        public bool IsTempCarIn
        {
            get => _isTempCarIn; set
            {
                _isTempCarIn = value;
                NotifyPropertyChange(() => IsTempCarIn);
            }
        }


        /// <summary>
        /// IsTempCarConfirm
        /// </summary>
        public bool IsTempCarConfirm
        {
            get => _isTempCarConfirm; set
            {
                _isTempCarConfirm = value;
                NotifyPropertyChange(() => IsTempCarConfirm);
            }
        }


        /// <summary>
        /// IsTempCarZeoPayOut
        /// </summary>
        public bool IsTempCarZeoPayOut
        {
            get => _isTempCarZeoPayOut; set
            {
                _isTempCarZeoPayOut = value;
                NotifyPropertyChange(() => IsTempCarZeoPayOut);
            }
        }


        /// <summary>
        /// IsNonVehicleIn
        /// </summary>
        public bool IsNonVehicleIn
        {
            get => _isNonVehicleIn; set
            {
                _isNonVehicleIn = value;
                NotifyPropertyChange(() => IsNonVehicleIn);
            }
        }


        /// <summary>
        /// NoNumberOptions
        /// </summary>
        public NoNumberOptions NoNumberOptions
        {
            get => _noNumberOptions; set
            {
                _noNumberOptions = value;
                NotifyPropertyChange(() => NoNumberOptions);
            }
        }






        //// custom codes

        //// custom codes end
    }
}