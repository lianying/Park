using Park.CarPorts.Dtos;
using Park.CarTypeses.Dtos;
using Park.MonthFees.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Childers.ViewModels
{
    public class CarPortTimeExTenstionViewModel : NotifyPropertyChangeBase<long>
    {
        private CarPortListDto _carPortListDto;
        private int _extensionCount;
        private List<CarTypesListDto> _getCarTypesListDtos;
        private CarTypesListDto _selecetedCarTypeDto;
        private decimal _receivable;
        private decimal _actualHarvest;
        private string _remkar;
        private DateTime _newDate;
        private MonthFeeListDto _selectedMothFee;

        public CarPortListDto CarPortListDto
        {
            get => _carPortListDto; set
            {
                _carPortListDto = value;
                NotifyPropertyChange(() => CarPortListDto);
            }
        }

        public int ExtensionCount
        {
            get => _extensionCount; set
            {
                _extensionCount = value;
                NotifyPropertyChange(() => ExtensionCount);
            }
        }

        public List<CarTypesListDto> CarTypesListDtos
        {
            get => _getCarTypesListDtos; set
            {
                _getCarTypesListDtos = value;
                NotifyPropertyChange(() => CarTypesListDtos);
            }
        }

        public MonthFeeListDto SelectedMothFee
        {
            get => _selectedMothFee; set
            {
                _selectedMothFee = value;
                NotifyPropertyChange(() => SelectedMothFee);
            }
        }

        //public CarTypesListDto SelecetedCarTypeDto
        //{
        //    get => _selecetedCarTypeDto; set
        //    {
        //        _selecetedCarTypeDto = value;
        //        NotifyPropertyChange(() => SelecetedCarTypeDto);
        //    }
        //}



        public decimal Receivable
        {
            get
            {
                return SelectedMothFee?.Month * ExtensionCount ?? 0;
            }
            set
            {
                _receivable = value;
                NotifyPropertyChange(() => Receivable);
            }
        }


        public decimal ActualHarvest
        {
            get => _actualHarvest; set
            {
                _actualHarvest = value;
                NotifyPropertyChange(() => ActualHarvest);
            }
        }



        public string Remark
        {
            get => _remkar; set
            {
                _remkar = value;
                NotifyPropertyChange(() => Remark);
            }
        }


        public DateTime NewDate
        {
            get => _newDate; set
            {
                _newDate = value;
                NotifyPropertyChange(() => NewDate);
            }
        }









    }
}
