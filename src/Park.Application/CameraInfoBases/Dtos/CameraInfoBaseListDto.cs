

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Park.Entitys;
using Park.Interfaces;
using Park.Enum;
using Park.ParkEntranceses.Dtos;
using System.IO;

namespace Park.CameraInfoBases.Dtos
{
    public class CameraInfoBaseListDto : EntityDto,ISynchronize
    {
        public string Name { get; set; }
/// <summary>
/// Ip
/// </summary>
public string Ip { get; set; }


/// <summary>
/// Port
/// </summary>
public long Port { get; set; }


/// <summary>
/// UserName
/// </summary>
public string UserName { get; set; }


/// <summary>
/// Password
/// </summary>
public string Password { get; set; }


/// <summary>
/// EquipmentManufacturers
/// </summary>
public EquipmentManufacturers EquipmentManufacturers { get; set; }


/// <summary>
/// ParkEntrance
/// </summary>
public ParkEntrancesListDto ParkEntrance { get; set; }


/// <summary>
/// EntranceId
/// </summary>
public long EntranceId { get; set; }


/// <summary>
/// DeviceType
/// </summary>
public DeviceType DeviceType { get; set; }


/// <summary>
/// Sort
/// </summary>
public int Sort { get; set; }


/// <summary>
/// LoginId
/// </summary>
public long LoginId { get; set; }


/// <summary>
/// DeviceStatus
/// </summary>
public DeviceStatus DeviceStatus { get; set; }


/// <summary>
/// CalBackData
/// </summary>
public object CalBackData { get; set; }


/// <summary>
/// Handler
/// </summary>
public IntPtr? Handler { get; set; }


/// <summary>
/// Image
/// </summary>
public Stream Image { get; set; }


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