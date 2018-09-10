using Park.Authorization.Users.Dto;
using Park.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Childers.ViewModels
{
    public class AddSysUserViewModel : NotifyPropertyChangeBase
    {
        private CreateOrUpdateUserInput _userInput;

        public CreateOrUpdateUserInput UserInput
        {
            get => _userInput; set
            {
                _userInput = value;
                NotifyPropertyChange(() => UserInput);
            }
        }
    }
}
