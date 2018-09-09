using Park.CameraInfoBases.Dtos;
using Park.ParkEntranceses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class CameraManagerViewModel : NotifyPropertyChangeBase
    {
        private string _filter;
        private CameraInfoBaseListDto _selectedCamera;
        private ObservableCollection<ParkEntrancesListDto> _entrancesListDtos;

        public string Filter
        {
            get => _filter; set
            {
                _filter = value;
                NotifyPropertyChange(() => Filter);
            }
        }

        public CameraInfoBaseListDto SelectedCamera
        {
            get => _selectedCamera; set
            {
                _selectedCamera = value;
                NotifyPropertyChange(() => SelectedCamera);
            }
        }

        public ObservableCollection<ParkEntrancesListDto> EntrancesListDtos
        {
            get => _entrancesListDtos; set
            {
                _entrancesListDtos = value;
                NotifyPropertyChange(() => EntrancesListDtos);
            }
        }


    }
}
