using Park.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.CarTypeses.Dtos
{
    public class CarTypeTreeModel : NotifyPropertyChangeBase<long>
    {
        private CarType _carType;
        private ObservableCollection<CarTypesListDto> _carTypesListDtos;

        public CarType CarType
        {
            get => _carType; set
            {
                _carType = value;
                NotifyPropertyChange(() => CarType);
            }
        }

        public ObservableCollection<CarTypesListDto> CarTypesListDtos
        {
            get => _carTypesListDtos; set
            {
                _carTypesListDtos = value;
                NotifyPropertyChange(() => CarTypesListDtos);
            }
        }


    }
}
