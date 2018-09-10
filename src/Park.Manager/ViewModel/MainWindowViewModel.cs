using Park.Authorization.Users;
using Park.ParkBox.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.ViewModel
{
    public class MainWindowViewModel : NotifyPropertyChangeBase
    {

        public List<Menu> Menus { get; set; }
        private ParkDto _selectParkDto;
        private User _user;

        public ParkDto SelectParkDto
        {
            get { return _selectParkDto; }
            set
            {
                _selectParkDto = value;

            }
        }

        public User User
        {
            get => _user; set
            {
                _user = value;
                NotifyPropertyChange(() => User);
            }
        }










    }
}
