

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.CarPorts.Dtos;
using Park.Entitys.CarUsers;
using Park.Enum;

namespace  Park.CarUserses.Dtos
{
    public class CarUsersEditDto 
    {
        private string _name;
        private string _phone;
        private string _remark;
        private string _contact;
        private long _areaId;
        private CarUserGroups.Dtos.CarUserGroupListDto _userArea;


        public long? Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [MaxLength(285, ErrorMessage = "Name超出最大长度")]
        [MinLength(0, ErrorMessage = "Name小于最小长度")]
        public string Name
        {
            get => _name; set
            {
                _name = value;
            }
        }


        /// <summary>
        /// Sex
        /// </summary>
        public Sex Sex { get; set; }


        /// <summary>
        /// Phone
        /// </summary>
        public string Phone
        {
            get => _phone; set
            {
                _phone = value;
            }
        }


        /// <summary>
        /// ParkArea
        /// </summary>
        public CarUserGroups.Dtos.CarUserGroupListDto UserArea
        {
            get => _userArea; set
            {
                _userArea = value;
            }
        }

        public UserType UserType { get; set; }


        /// <summary>
        /// AreaId
        /// </summary>
        public long GroupId
        {
            get => _areaId; set
            {
                _areaId = value;
            }
        }


        /// <summary>
        /// ParkId
        /// </summary>
        public int ParkId { get; set; }


        /// <summary>
        /// Park
        /// </summary>
        public Park.Parks.Park.Dto.CreateParkDto Park { get; set; }


        /// <summary>
        /// CarPorts
        /// </summary>
        public ICollection<CarPortListDto> CarPorts { get; set; }


        /// <summary>
        /// CarNumbers
        /// </summary>
        public ICollection<CarNumberses.Dtos.CarNumbersListDto> CarNumbers { get; set; }


        /// <summary>
        /// FullInType
        /// </summary>
        public FullInType FullInType { get; set; }


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


        public string Remark
        {
            get => _remark; set
            {
                _remark = value;
            }
        }

        public string Contact
        {
            get => _contact; set
            {
                _contact = value;
            }
        }




        //// custom codes

        //// custom codes end
    }
}