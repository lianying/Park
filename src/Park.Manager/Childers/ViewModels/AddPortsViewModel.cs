using Park.CarPorts.Dtos;
using Park.CarTypeses.Dtos;
using Park.ParkAreases.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Childers.ViewModels
{
    public class AddPortsViewModel : NotifyPropertyChangeBase<long>
    {
        private int _carportCount;
        private CarPortEditDto _carPortEditDto;

        public int CarportCount
        {
            get => _carportCount; set
            {
                _carportCount = value;
                NotifyPropertyChange(() => CarportCount);
            }
        }


        public CarPortEditDto CarPortEditDto
        {
            get
            {
                if (_carPortEditDto == null)
                    _carPortEditDto = new CarPortEditDto();
                return _carPortEditDto;
            }
            set
            {
                _carPortEditDto = value;
                NotifyPropertyChange(() => CarPortEditDto);
            }
        }


        private ObservableCollection<ParkAreaDto> parkAreaDtos;
        public ObservableCollection<ParkAreaDto> ParkAreaDtos
        {
            get { return parkAreaDtos; }
            set
            {
                parkAreaDtos = value;
                NotifyPropertyChange(() => ParkAreaDtos);
            }
        }

        private ObservableCollection<CarTypesListDto> carTypesDto;
        public ObservableCollection<CarTypesListDto> CarTypesDto { get { return carTypesDto; } set { carTypesDto = value; NotifyPropertyChange(() => CarTypesDto); } }



    }
}
