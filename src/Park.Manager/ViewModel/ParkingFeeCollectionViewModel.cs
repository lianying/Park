using Park.CarPorts.Dtos;
using Park.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class ParkingFeeCollectionViewModel : NotifyPropertyChangeBase
    {
        private string _carportSerial;
        private string _carNumber;
        private string _userName;
        private string _contact;
        private string _phone;
        private ValidityPeriod _validityPeriod;
        private ObservableCollection<CarPortListDto> _portListDtos;

        public string CarportSerial
        {
            get => _carportSerial; set
            {
                _carportSerial = value;
                NotifyPropertyChange(() => CarportSerial);
            }
        }

        public string CarNumber
        {
            get => _carNumber; set
            {
                _carNumber = value;
                NotifyPropertyChange(() => CarNumber);
            }
        }

        public string UserName
        {
            get => _userName; set
            {
                _userName = value;
                NotifyPropertyChange(() => UserName);
            }
        }

        public string Contact
        {
            get => _contact; set
            {
                _contact = value;
                NotifyPropertyChange(() => Contact);
            }
        }

        public string Phone
        {
            get => _phone; set
            {
                _phone = value;
                NotifyPropertyChange(() => Phone);
            }
        }


        public ValidityPeriod ValidityPeriod
        {
            get => _validityPeriod; set
            {
                _validityPeriod = value;
                NotifyPropertyChange(() => ValidityPeriod);
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






    }
}
