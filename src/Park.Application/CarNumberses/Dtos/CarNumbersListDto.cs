

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Park.Entitys.CarUsers;
using Abp.Domain.Entities;
using Park.Interfaces;
using Park.CarUserses.Dtos;

namespace Park.CarNumberses.Dtos
{
    public class CarNumbersListDto : NotifyPropertyChangeBase<long>, IHasCreationTime, IModificationAudited, IHasModificationTime, IDeletionAudited, IHasDeletionTime, ISoftDelete, ISynchronize
    {
        private string _carNumber;
        private CarUsersListDto _carUser;
        private string _remark;
        private string _phone;
        private string _contact;

        /// <summary>
        /// CarNumber
        /// </summary>
        [RegularExpression("([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z0]{1}(([0-9]{5}[DF])|([DF]([A-HJ-NP-Z0-9])[0-9]{4})))|([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z0]{1}[A-HJ-NP-Z0-9]{4}[A-HJ-NP-Z0-9挂学警港澳]{1})", ErrorMessage = "CarNumber格式错误")]
        public string CarNumber
        {
            get => _carNumber; set
            {
                _carNumber = value;
                NotifyPropertyChange(() => CarNumber);
            }
        }


        /// <summary>
        /// CarUser
        /// </summary>
        public CarUsersListDto CarUser
        {
            get => _carUser; set
            {
                _carUser = value;
                NotifyPropertyChange(() => CarUser);
            }
        }


        public virtual string Contact
        {
            get => _contact; set
            {
                _contact = value;
                NotifyPropertyChange(() => Contact);
            }
        }

        public virtual string Phone
        {
            get => _phone; set
            {
                _phone = value;
                NotifyPropertyChange(() => Phone);
            }
        }

        public virtual string Remark
        {
            get => _remark; set
            {
                _remark = value;
                NotifyPropertyChange(() => Remark);
            }
        }


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