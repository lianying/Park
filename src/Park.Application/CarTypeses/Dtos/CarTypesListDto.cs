

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Park.Entitys.CarTypes;
using Park.Enum;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Park.CarTypeses.Dtos
{
    public class CarTypesListDto : NotifyPropertyChangeBase<long>, IMayHaveTenant
    {
        private bool _isSelected;

        /// <summary>
        /// Name
        /// </summary>
        [MaxLength(100, ErrorMessage = "Name超出最大长度")]
        [MinLength(0, ErrorMessage = "Name小于最小长度")]
        public string Name { get; set; }


        /// <summary>
        /// CustomName
        /// </summary>
        [MaxLength(100, ErrorMessage = "CustomName超出最大长度")]
        [MinLength(0, ErrorMessage = "CustomName小于最小长度")]
        public string CustomName { get; set; }


        /// <summary>
        /// Warring
        /// </summary>
        [MaxLength(999999, ErrorMessage = "Warring超出最大长度")]
        [MinLength(1, ErrorMessage = "Warring小于最小长度")]
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



        [NotMapped]
        //// custom codes
        public bool IsSelected
        {
            get => _isSelected; set
            {
                _isSelected = value;
                NotifyPropertyChange(() => IsSelected);
            }
        }
        //// custom codes end
    }
}