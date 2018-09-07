

using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Park.Entitys.CarUsers;

namespace  Park.CarNumberses.Dtos
{
    public class CarNumbersEditDto
    {
/// <summary>
/// Id
/// </summary>
public long? Id { get; set; }


/// <summary>
/// CarNumber
/// </summary>
[RegularExpression("([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z0]{1}(([0-9]{5}[DF])|([DF]([A-HJ-NP-Z0-9])[0-9]{4})))|([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z0]{1}[A-HJ-NP-Z0-9]{4}[A-HJ-NP-Z0-9挂学警港澳]{1})",ErrorMessage="CarNumber格式错误")]
public string CarNumber { get; set; }


/// <summary>
/// CarUser
/// </summary>
public CarUsers CarUser { get; set; }


        public virtual string Contact { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Remark { get; set; }


        /// <summary>
        /// CarUserId
        /// </summary>
        public long CarUserId { get; set; }


/// <summary>
/// IsSuccess
/// </summary>
public bool IsSuccess { get; set; }


/// <summary>
/// CloudId
/// </summary>
public string CloudId { get; set; }


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






		//// custom codes
 
        //// custom codes end
    }
}