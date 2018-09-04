using Park.CarNumberses.Dtos;
using Park.CarPorts.Dtos;
using Park.CarUserGroups.Dtos;
using Park.CarUserses.Dtos;
using Park.ParkAreases.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class UserManagerViewModel : NotifyPropertyChangeBase
    {
        private ObservableCollection<ParkAreases.Dtos.ParkAreaDto> _parkAreaDtos;
        private string _filter;
        private ObservableCollection<CarNumberses.Dtos.CarNumbersListDto> _carNumbersDto;
        private ObservableCollection<CarPortListDto> _portListDtos;
        private CarUsersListDto _selectedUser;
        private ObservableCollection<CarUserGroupListDto> _groupListDtos;

        public string Filter
        {
            get => _filter; set
            {
                _filter = value;
                NotifyPropertyChange(() => Filter);
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


        public ObservableCollection<CarNumbersListDto> CarNumbersDtos
        {
            get => _carNumbersDto; set
            {
                _carNumbersDto = value;
                NotifyPropertyChange(() => CarNumbersDtos);
            }
        }

        public ObservableCollection<CarPortListDto> PortListDtos
        {
            get => _portListDtos; set
            {
                _portListDtos = value;
                NotifyPropertyChange(() => PortListDtos);
            }
        }


        public CarUsersListDto SelectedUser
        {
            get => _selectedUser; set
            {
                _selectedUser = value;
                NotifyPropertyChange(() => SelectedUser);
            }
        }


        public ObservableCollection<CarUserGroupListDto> GroupListDtos
        {
            get => _groupListDtos; set
            {
                _groupListDtos = value;
                NotifyPropertyChange(() => GroupListDtos);
            }
        }











    }
}
