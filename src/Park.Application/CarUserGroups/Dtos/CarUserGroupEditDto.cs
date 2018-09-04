

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.Entitys.CarUsers;
using Park.ParkAreases.Dtos;

namespace  Park.CarUserGroups.Dtos
{
    public class CarUserGroupEditDto
    {
/// <summary>
/// Id
/// </summary>
public int? Id { get; set; }


/// <summary>
/// GroupName
/// </summary>
public string GroupName { get; set; }


/// <summary>
/// ParkArea
/// </summary>
[MaxLength(100, ErrorMessage="ParkArea超出最大长度")]
[MinLength(2, ErrorMessage="ParkArea小于最小长度")]
public ParkAreaDto ParkArea { get; set; }


/// <summary>
/// CreationTime
/// </summary>
public DateTime CreationTime { get; set; }


/// <summary>
/// IsSuccess
/// </summary>
public bool IsSuccess { get; set; }


/// <summary>
/// CloudId
/// </summary>
public string CloudId { get; set; }






		//// custom codes
 
        //// custom codes end
    }
}