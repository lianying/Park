

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.Entitys.CarUsers;
using Park.Enum;

namespace  Park.CarUserses.Dtos
{
    public class CarUsersEditDto
    {
/// <summary>
/// Id
/// </summary>
public long? Id { get; set; }


/// <summary>
/// Name
/// </summary>
[MaxLength(285, ErrorMessage="Name超出最大长度")]
[MinLength(0, ErrorMessage="Name小于最小长度")]
public string Name { get; set; }


/// <summary>
/// Sex
/// </summary>
public Sex Sex { get; set; }


/// <summary>
/// Phone
/// </summary>
public string Phone { get; set; }


/// <summary>
/// ParkArea
/// </summary>
public Park.Entitys.ParkAreas.ParkAreas ParkArea { get; set; }


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
public Park.Entitys.Parks.ParkSet Park { get; set; }


/// <summary>
/// CarPorts
/// </summary>
public ICollection<CarPort> CarPorts { get; set; }


/// <summary>
/// CarNumbers
/// </summary>
[MinLength(0, ErrorMessage="CarNumbers小于最小长度")]
public ICollection<CarNumbers> CarNumbers { get; set; }


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
            get; set;
        }



        //// custom codes

        //// custom codes end
    }
}