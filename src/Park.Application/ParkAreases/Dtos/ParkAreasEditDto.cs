

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.Entitys.ParkAreas;
using Park.Entitys.Parks;

namespace  Park.ParkAreases.Dtos
{
    public class ParkAreasEditDto
    {
/// <summary>
/// Id
/// </summary>
public long? Id { get; set; }


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


/// <summary>
/// ParkAreaRentableCarports
/// </summary>
public int ParkAreaRentableCarports { get; set; }


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