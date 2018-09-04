

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.Entitys.ParkEntrances;
using Park.Enum;

namespace  Park.ParkEntrancePermissions.Dtos
{
    public class ParkEntrancePermissionEditDto
    {
/// <summary>
/// Id
/// </summary>
public long? Id { get; set; }


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
public bool IsTempCarIn { get; set; }


/// <summary>
/// IsTempCarConfirm
/// </summary>
public bool IsTempCarConfirm { get; set; }


/// <summary>
/// IsTempCarZeoPayOut
/// </summary>
public bool IsTempCarZeoPayOut { get; set; }


/// <summary>
/// IsNonVehicleIn
/// </summary>
public bool IsNonVehicleIn { get; set; }


/// <summary>
/// NoNumberOptions
/// </summary>
public NoNumberOptions NoNumberOptions { get; set; }






		//// custom codes
 
        //// custom codes end
    }
}