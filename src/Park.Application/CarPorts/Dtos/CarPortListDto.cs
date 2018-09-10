

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Park.Entitys.CarUsers;
using Abp.Domain.Entities;
using Park.Entitys.ParkAreas;
using Park.Entitys.ParkLevels;
using Park.Entitys.CarTypes;
using Park.Enum;
using Park.Interfaces;
using Park.ParkAreases.Dtos;
using Park.CarTypeses.Dtos;

namespace Park.CarPorts.Dtos
{
    public class CarPortListDto : NotifyPropertyChangeBase<long>, IHasCreationTime, IModificationAudited, IHasModificationTime, IDeletionAudited, IHasDeletionTime, ISoftDelete, ISynchronize
    {
        private bool _isSelected;

        /// <summary>
        /// CarportSerial
        /// </summary>
        public string CarportSerial { get; set; }


        /// <summary>
        /// AreaId
        /// </summary>
        public long AreaId { get; set; }


        /// <summary>
        /// ParkArea
        /// </summary>
        public ParkAreaDto ParkArea { get; set; }


        /// <summary>
        /// LevelId
        /// </summary>
        public long LevelId { get; set; }


        /// <summary>
        /// ParkLevel
        /// </summary>
        //public ParkLevels ParkLevel { get; set; }


        /// <summary>
        /// StartTime
        /// </summary>
        public DateTime StartTime { get; set; }


        /// <summary>
        /// EndTime
        /// </summary>
        public DateTime EndTime { get; set; }


        /// <summary>
        /// IsRent
        /// </summary>
        public bool IsRent { get; set; }


        /// <summary>
        /// CarPortType
        /// </summary>
        public CarTypesListDto CarPortType { get; set; }


        /// <summary>
        /// CarPortTypeId
        /// </summary>
        public long CarPortTypeId { get; set; }


        /// <summary>
        /// IsSharing
        /// </summary>
        public bool IsSharing { get; set; }


        /// <summary>
        /// HasChargingPile
        /// </summary>
        public bool HasChargingPile { get; set; }


        /// <summary>
        /// IsRealCarports
        /// </summary>
        public bool IsRealCarports { get; set; }


        /// <summary>
        /// CarUser
        /// </summary>
        public CarUsers CarUser { get; set; }


        /// <summary>
        /// CarUserId
        /// </summary>
        public long? CarUserId { get; set; }


        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }


        /// <summary>
        /// LastModifierUserId
        /// </summary>
        public long? LastModifierUserId { get; set; }


        /// <summary>
        /// LastModificationTime
        /// </summary>
        public DateTime? LastModificationTime { get; set; }


        /// <summary>
        /// DeleterUserId
        /// </summary>
        public long? DeleterUserId { get; set; }


        /// <summary>
        /// DeletionTime
        /// </summary>
        public DateTime? DeletionTime { get; set; }


        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }


        /// <summary>
        /// IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }


        /// <summary>
        /// CloudId
        /// </summary>
        public string CloudId { get; set; }


        /// <summary>
        /// RentSellType
        /// </summary>
        public CarPortType RentSellType { get; set; }

        public virtual int ParkId { get; set; }


        public string RentSellTypeStaticString
        {
            get
            {
                if (!CarUserId.HasValue)
                {
                    return "未租售";
                }
                else
                {
                    if (CarPortType.RentingSellingType == RentingSellingType.Rent)
                    {
                        return "已出租";
                    }
                    else
                    {
                        return "已出售";
                    }
                }
            }
        }


        public string Remark { get; set; }


        public bool IsSelected
        {
            get => _isSelected; set
            {
                _isSelected = value;
                NotifyPropertyChange(() => IsSelected);
            }
        }






        //// custom codes

        //// custom codes end
    }
}