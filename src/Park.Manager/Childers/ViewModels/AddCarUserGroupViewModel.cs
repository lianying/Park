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
    public class AddCarUserGroupViewModel : NotifyPropertyChangeBase
    {
        private ObservableCollection<ParkAreaDto> _parkAreaDtos;
        private CarUserGroups.Dtos.CarUserGroupEditDto _carUserGroup;

        public ObservableCollection<ParkAreaDto> ParkAreaDtos
        {
            get => _parkAreaDtos; set
            {
                _parkAreaDtos = value;
                NotifyPropertyChange(() => ParkAreaDtos);
            }
        }


        public CarUserGroups.Dtos.CarUserGroupEditDto CarUserGroup
        {
            get => _carUserGroup; set
            {
                _carUserGroup = value;
                NotifyPropertyChange(() => CarUserGroup);
            }
        }



    }
}
