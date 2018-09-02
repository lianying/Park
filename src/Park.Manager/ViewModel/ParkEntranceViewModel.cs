using Park.Enum;
using Park.ParkAreases.Dtos;
using Park.ParkEntranceses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class ParkEntranceViewModel : NotifyPropertyChangeBase
    {
        private ObservableCollection<ParkAreaDto> _parkAreaDtos;
        private ObservableCollection<ParkEntranceses.Dtos.ParkEntrancesListDto> _entrancesListDtos;
        private ParkEntrancesListDto _selectDto;
        private ObservableCollection<CarTypeses.Dtos.CarTypesListDto> _carTypesLists;

        public ObservableCollection<ParkAreaDto> ParkAreaDtos
        {
            get => _parkAreaDtos; set
            {
                _parkAreaDtos = value;
                NotifyPropertyChange(() => ParkAreaDtos);
            }
        }


        //public ObservableCollection<ParkEntranceses.Dtos.ParkEntrancesListDto> EntrancesListDtos
        //{
        //    get => _entrancesListDtos; set
        //    {
        //        _entrancesListDtos = value;
        //        NotifyPropertyChange(() => EntrancesListDtos);
        //    }
        //}

        public ParkEntrancesListDto SelectDto
        {
            get => _selectDto; set
            {
                _selectDto = value;
                NotifyPropertyChange(() => SelectDto);
            }
        }


        public ObservableCollection<CarTypeses.Dtos.CarTypesListDto> CarTypesLists
        {
            get => _carTypesLists; set
            {
                _carTypesLists = value;
                NotifyPropertyChange(() => CarTypesLists);
            }
        }

        private KeyValuePair<EntranceType, string> _selectedMyEnumType;
        private string _entranceName;

        public KeyValuePair<EntranceType, string> SelectedMyEnumType
        {
            get { return _selectedMyEnumType; }
            set
            {
                _selectedMyEnumType = value;
                NotifyPropertyChange("SelectedMyEnumType");
            }
        }

        public IEnumerable<KeyValuePair<EntranceType,string>> MyEnumTypeValues
        {
            get
            {
                return System.Enum.GetValues(typeof(EntranceType))
                    .Cast<EntranceType>()
                      .Select(value => new KeyValuePair<EntranceType, string>(value, (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description)
                       )
    .ToList();
            }
        }


        public string EntranceName
        {
            get => _entranceName; set
            {
                _entranceName = value;
                NotifyPropertyChange(() => EntranceName);
            }
        }








    }
}
