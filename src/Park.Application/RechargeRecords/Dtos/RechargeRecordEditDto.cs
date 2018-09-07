

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.Authorization.Users;
using Park.Entitys.CarUsers;

namespace  Park.RechargeRecords.Dtos
{
    public class RechargeRecordEditDto
    {
/// <summary>
/// Id
/// </summary>
public long? Id { get; set; }


/// <summary>
/// Receivable
/// </summary>
public decimal Receivable { get; set; }


/// <summary>
/// ActualHarvest
/// </summary>
public decimal ActualHarvest { get; set; }


/// <summary>
/// UserId
/// </summary>
public long UserId { get; set; }


/// <summary>
/// CarPortId
/// </summary>
public long CarPortId { get; set; }


/// <summary>
/// CarUser
/// </summary>
public CarUsers CarUser { get; set; }


/// <summary>
/// CarPort
/// </summary>
public CarPort CarPort { get; set; }


/// <summary>
/// CarNumbers
/// </summary>
public string CarNumbers { get; set; }


/// <summary>
/// ExtensionCount
/// </summary>
public int ExtensionCount { get; set; }


/// <summary>
/// OldDate
/// </summary>
public DateTime OldDate { get; set; }


/// <summary>
/// NewDate
/// </summary>
public DateTime NewDate { get; set; }


/// <summary>
/// Remark
/// </summary>
public string Remark { get; set; }


/// <summary>
/// SysUserId
/// </summary>
public long SysUserId { get; set; }


/// <summary>
/// User
/// </summary>
public User User { get; set; }


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