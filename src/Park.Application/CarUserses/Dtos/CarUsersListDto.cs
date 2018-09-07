

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Park.Entitys.CarUsers;
using Park.Enum;
using Abp.Domain.Entities;
using Park.Interfaces;
using Park.CarPorts.Dtos;

namespace Park.CarUserses.Dtos
{
    public class CarUsersListDto : NotifyPropertyChangeBase<long>, IHasCreationTime, IModificationAudited, IHasModificationTime, IDeletionAudited, IHasDeletionTime, ISoftDelete, ISynchronize
    {
        private string _name;
        private string _phone;
        private string _remark;
        private string _contact;

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
                NotifyPropertyChange(() => Name);
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
                NotifyPropertyChange(() => Phone);
            }
        }


        /// <summary>
        /// ParkArea
        /// </summary>
        public CarUserGroups.Dtos.CarUserGroupListDto UserArea { get; set; }

        public UserType UserType { get; set; }


        /// <summary>
        /// AreaId
        /// </summary>
        public long AreaId { get; set; }


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
        [MinLength(0, ErrorMessage = "CarNumbers小于最小长度")]
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
                NotifyPropertyChange(() => Remark);
            }
        }

        public string Contact
        {
            get => _contact; set
            {
                _contact = value;
                NotifyPropertyChange(() => Contact);
            }
        }




        //// custom codes

        //// custom codes end
    }
}