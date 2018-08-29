

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Park.Entitys.ParkEntrances;
using Park.Entitys.ParkLevels;
using Park.Enum;
using Park.Interfaces;
using Abp.Domain.Entities;

namespace Park.ParkEntranceses.Dtos
{
    public class ParkEntrancesListDto : NotifyPropertyChangeBase<long>, ISynchronize, IAudited, IMayHaveTenant
    {
        private string _entranceName;

        /// <summary>
        /// EntranceName
        /// </summary>
        [MaxLength(200, ErrorMessage = "EntranceName超出最大长度")]
        [MinLength(0, ErrorMessage = "EntranceName小于最小长度")]
        public string EntranceName
        {
            get => _entranceName; set
            {
                _entranceName = value;
                NotifyPropertyChange(() => EntranceName);
            }

        }


        /// <summary>
        /// EntranceType
        /// </summary>
        public EntranceType EntranceType { get; set; }


        /// <summary>
        /// ParkLevel
        /// </summary>
        public ParkLevels ParkLevel { get; set; }


        /// <summary>
        /// ParkLevelId
        /// </summary>
        public long ParkLevelId { get; set; }


        /// <summary>
        /// PermissionId
        /// </summary>
        public long PermissionId { get; set; }


        /// <summary>
        /// ParkEntrancePermission
        /// </summary>
        public ParkEntrancePermission ParkEntrancePermission { get; set; }


        /// <summary>
        /// IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }


        /// <summary>
        /// CloudId
        /// </summary>
        public string CloudId { get; set; }


        /// <summary>
        /// CreatorUserId
        /// </summary>
        public long? CreatorUserId { get; set; }


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
        /// TenantId
        /// </summary>
        public int? TenantId { get; set; }


        /// <summary>
        /// ParkAreas
        /// </summary>
        public ParkAreases.Dtos.ParkAreaDto ParkAreas { get; set; }


        /// <summary>
        /// AreaId
        /// </summary>
        public long AreaId { get; set; }






        //// custom codes

        //// custom codes end
    }
}