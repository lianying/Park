

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Park.Entitys.ParkAreas;
using Park.Interfaces;
using Abp.Domain.Entities;
using Park.Entitys.Parks;

namespace Park.ParkAreases.Dtos
{
    public class ParkAreasListDto : EntityDto<long>,ISynchronize,IAudited,IMayHaveTenant
    {

/// <summary>
/// Park
/// </summary>
public ParkBase Park { get; set; }


/// <summary>
/// ParkId
/// </summary>
public int ParkId { get; set; }


/// <summary>
/// AreaName
/// </summary>
[MaxLength(1000, ErrorMessage="AreaName超出最大长度")]
public string AreaName { get; set; }


/// <summary>
/// ParkAreaCarports
/// </summary>
public int ParkAreaCarports { get; set; }


        public virtual int ParkAreaTempCarports { get; set; }

        /// <summary>
        /// 固定车位
        /// </summary>
        public virtual int ParkAreaFixedCarports { get; set; }

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






		//// custom codes
 
        //// custom codes end
    }
}