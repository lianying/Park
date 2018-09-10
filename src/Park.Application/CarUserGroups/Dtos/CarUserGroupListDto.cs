

using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Park.Entitys.CarUsers;
using Park.Interfaces;
using Park.ParkAreases.Dtos;
using System.Collections.ObjectModel;

namespace Park.CarUserGroups.Dtos
{
    public class CarUserGroupListDto : NotifyPropertyChangeBase<int>, IHasCreationTime, ISynchronize
    {
        private string _groupName;
        private ObservableCollection<CarUserses.Dtos.CarUsersListDto> _usersListDtos;

        /// <summary>
        /// GroupName
        /// </summary>
        public string GroupName
        {
            get => _groupName; set
            {
                _groupName = value;
                NotifyPropertyChange(() => GroupName);
            }
        }


        /// <summary>
        /// ParkArea
        /// </summary>
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



        public ObservableCollection<CarUserses.Dtos.CarUsersListDto> UsersListDtos
        {
            get => _usersListDtos; set
            {
                _usersListDtos = value;
                NotifyPropertyChange(() => UsersListDtos);
            }
        }




        //// custom codes

        //// custom codes end
    }
}