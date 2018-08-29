

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.Entitys.CarTypes;
using Park.Enum;

namespace  Park.CarTypeses.Dtos
{
    public class CarTypesEditDto
    {
/// <summary>
/// Id
/// </summary>
public long? Id { get; set; }


/// <summary>
/// Name
/// </summary>
[MaxLength(100, ErrorMessage="Name超出最大长度")]
[MinLength(0, ErrorMessage="Name小于最小长度")]
public string Name { get; set; }


/// <summary>
/// CustomName
/// </summary>
[MaxLength(100, ErrorMessage="CustomName超出最大长度")]
[MinLength(0, ErrorMessage="CustomName小于最小长度")]
public string CustomName { get; set; }


/// <summary>
/// Warring
/// </summary>
[MaxLength(999999, ErrorMessage="Warring超出最大长度")]
[MinLength(1, ErrorMessage="Warring小于最小长度")]
public decimal Warring { get; set; }


/// <summary>
/// Category
/// </summary>
public CarType Category { get; set; }


/// <summary>
/// TenantId
/// </summary>
public int? TenantId { get; set; }




        public RentingSellingType RentingSellingType { get; set; }


        //// custom codes

        //// custom codes end
    }
}