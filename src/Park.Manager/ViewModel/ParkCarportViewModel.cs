using Park.CarPorts.Dtos;
using Park.CarTypeses.Dtos;
using Park.ParkAreases.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class ParkCarportViewModel:NotifyPropertyChangeBase
    {
        private string carportNumber;
        public string CarportNumber
        {
            get { return carportNumber; }
            set
            {
                carportNumber = value;
                NotifyPropertyChange(() => CarportNumber);
            }
        }

        private int totleCount;
        public int TotleCount
        {
            get { return totleCount; }
            set
            {
                totleCount = value;

                NotifyPropertyChange(() => TotleCount);
            }
        }

        private int rentCount;
        public int RentCount
        {
            get { return rentCount; }
            set
            {
                rentCount = value;

                NotifyPropertyChange(() => RentCount);
            }
        }

        private int sellCount;
        public int SellCount
        {
            get { return sellCount; }
            set
            {
                sellCount = value;

                NotifyPropertyChange(() => SellCount);
            }
        }


        private int remainingCount;

        public int RemainingCount { get { return remainingCount; } set { remainingCount = value; NotifyPropertyChange(() => RemainingCount); } }


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

        private ObservableCollection<CarPortListDto> carPortListDtos;
        public ObservableCollection<CarPortListDto> CarPortListDtos
        {
            get { return carPortListDtos; }
            set
            {
                carPortListDtos = value;

                NotifyPropertyChange(() => CarPortListDtos);
            }
        }


        


    }
}
