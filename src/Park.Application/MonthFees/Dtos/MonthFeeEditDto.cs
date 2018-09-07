

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.CarTypeses.Dtos;
using Park.Entitys.FareRules;

namespace  Park.MonthFees.Dtos
{
    public class MonthFeeEditDto
    {
/// <summary>
/// Id
/// </summary>
public int? Id { get; set; }


/// <summary>
/// Name
/// </summary>
[MaxLength(200, ErrorMessage="Name超出最大长度")]
[MinLength(1, ErrorMessage="Name小于最小长度")]
public string Name { get; set; }


/// <summary>
/// CarTypes
/// </summary>
public CarTypesListDto CarTypes { get; set; }


/// <summary>
/// CarTypeId
/// </summary>
public long CarTypeId { get; set; }


/// <summary>
/// Month
/// </summary>
public decimal Month { get; set; }


/// <summary>
/// Quarter
/// </summary>
public decimal Quarter { get; set; }


/// <summary>
/// HalfYear
/// </summary>
public decimal HalfYear { get; set; }


/// <summary>
/// Year
/// </summary>
public decimal Year { get; set; }


        public string CloudId { get; set; }

        public bool IsSuccess { get; set; }






		//// custom codes
 
        //// custom codes end
    }
}