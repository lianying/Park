using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Authorization.Users.Dto
{
    public class CreateOrUpdateUserInput : NotifyPropertyChangeBase<long>
    {
        private string[] _assignedRoleNames;
        private bool _sendActivationEmail;
        private bool _setRandomPassword;
        private UserEditDto _user;

        [Required]
        public UserEditDto User
        {
            get => _user; set
            {
                _user = value;
                NotifyPropertyChange(() => User);
            }
        }

        [Required]
        public string[] AssignedRoleNames
        {
            get => _assignedRoleNames; set
            {
                _assignedRoleNames = value;
                NotifyPropertyChange(() => AssignedRoleNames);
            }
        }

        public bool SendActivationEmail
        {
            get => _sendActivationEmail; set
            {
                _sendActivationEmail = value;
                NotifyPropertyChange(() => SendActivationEmail);
            }
        }

        public bool SetRandomPassword
        {
            get => _setRandomPassword; set
            {
                _setRandomPassword = value;
                NotifyPropertyChange(() => SetRandomPassword);
            }
        }
    }
}
