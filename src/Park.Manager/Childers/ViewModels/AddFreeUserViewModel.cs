using Park.CarPorts.Dtos;
using Park.CarUserGroups.Dtos;
using Park.ParkAreases.Dtos;
using Park.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Childers.ViewModels
{
    public class AddFreeUserViewModel : NotifyPropertyChangeBase
    {
        private ObservableCollection<CarUserGroups.Dtos.CarUserGroupListDto> _getCarUserGroupLists;
        private CarUserGroupListDto _selectedUserGroup;
        private string _userName;
        private CarUserses.Dtos.CarUsersListDto _carUsersListDto;
        private ObservableCollection<ParkAreases.Dtos.ParkAreaDto> _parkAreaDtos;
        private ObservableCollection<CarPorts.Dtos.CarPortListDto> _portListDtos;
        private ParkAreaDto _selectedParkArea;
        private CarPortListDto _carPortListDto;
        private DateTime _endTime;

        /// <summary>
        /// 车牌号
        /// </summary>
        public List<string> CarNumbers { get; set; }


        public ObservableCollection<CarUserGroups.Dtos.CarUserGroupListDto> GetCarUserGroupLists
        {
            get => _getCarUserGroupLists; set
            {
                _getCarUserGroupLists = value;
                NotifyPropertyChange(() => GetCarUserGroupLists);
            }
        }


        public CarUserGroupListDto SelectedUserGroup
        {
            get => _selectedUserGroup; set
            {
                _selectedUserGroup = value;
                NotifyPropertyChange(() => SelectedUserGroup);
            }
        }


        public CarUserses.Dtos.CarUsersListDto CarUsersListDto
        {
            get => _carUsersListDto; set
            {
                _carUsersListDto = value;
                NotifyPropertyChange(() => CarUsersListDto);
            }
        }


        public ObservableCollection<ParkAreaDto> ParkAreaDtos
        {
            get => _parkAreaDtos; set
            {
                _parkAreaDtos = value;
                NotifyPropertyChange(() => ParkAreaDtos);
            }
        }

        public ParkAreaDto SelectedParkArea
        {
            get => _selectedParkArea; set
            {
                _selectedParkArea = value;
                NotifyPropertyChange(() => SelectedParkArea);
            }
        }

        public ObservableCollection<CarPorts.Dtos.CarPortListDto> PortListDtos
        {
            get => _portListDtos; set
            {
                _portListDtos = value;
                NotifyPropertyChange(() => PortListDtos);
            }
        }


        public CarPortListDto CarPortListDto
        {
            get => _carPortListDto; set
            {
                _carPortListDto = value;
                NotifyPropertyChange(() => CarPortListDto);
            }
        }


        public DateTime EndTime
        {
            get => _endTime; set
            {
                _endTime = value;
                NotifyPropertyChange(() => EndTime);
            }
        }


    }
}
