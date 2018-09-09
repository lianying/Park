using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class FreeUserViewModel : NotifyPropertyChangeBase
    {
        private ObservableCollection<CarUserses.Dtos.CarUsersListDto> _usersListDtos;
        private bool? _isExpired;
        private string _phone;
        private string _concat;
        private string _carNumber;
        private string _userName;

        public string UserName
        {
            get => _userName; set
            {
                _userName = value;
                NotifyPropertyChange(() => UserName);
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

        /// <summary>
        /// 联系人
        /// </summary>
        public string Concat
        {
            get => _concat; set
            {
                _concat = value;
                NotifyPropertyChange(() => Concat);
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

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool? IsExpired
        {
            get => _isExpired; set
            {
                _isExpired = value;
                NotifyPropertyChange(() => IsExpired);
            }
        }




        public ObservableCollection<CarUserses.Dtos.CarUsersListDto> UsersListDtos
        {
            get => _usersListDtos; set
            {
                _usersListDtos = value;
                NotifyPropertyChange(() => UsersListDtos);
            }
        }
    }
}
