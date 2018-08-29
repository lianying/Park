

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.Entitys.CarTypes;
using Park.Entitys.CarUsers;
using Park.Entitys.ParkAreas;
using Park.Entitys.ParkLevels;
using Park.Enum;

namespace  Park.CarPorts.Dtos
{
    public class CarPortEditDto : NotifyPropertyChangeBase<long>
    {
        private bool _hasChargingPile;
        private string _carportSerial;
        private string _remark;

        /// <summary>
        /// Id
        /// </summary>
        //public long? Id { get; set; }

        [Required]
        /// <summary>
        /// CarportSerial
        /// </summary>
        public string CarportSerial
        {
            get => _carportSerial;
            set
            {
                _carportSerial = value;
                NotifyPropertyChange(() => CarportSerial);
            }
        }


        /// <summary>
        /// AreaId
        /// </summary>
        public long AreaId { get; set; }


        /// <summary>
        /// ParkArea
        /// </summary>
        public ParkAreas ParkArea { get; set; }


        /// <summary>
        /// LevelId
        /// </summary>
        public long LevelId { get; set; }


        /// <summary>
        /// ParkLevel
        /// </summary>
        public ParkLevels ParkLevel { get; set; }


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
        public CarPortListDto CarPortType { get; set; }


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
        public bool HasChargingPile
        {
            get => _hasChargingPile; set
            {
                _hasChargingPile = value;
                NotifyPropertyChange(() => HasChargingPile);
            }
        }


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


        public string Remark
        {
            get => _remark; set
            {
                _remark = value;
                NotifyPropertyChange(() => Remark);
            }
        }





        //// custom codes

        //// custom codes end
    }
}