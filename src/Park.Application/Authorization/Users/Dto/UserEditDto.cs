using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Authorization.Users.Dto
{
    public class UserEditDto : NotifyPropertyChangeBase<long>, IPassivable
    {
        private string _name;
        private string _password;

        /// <summary>
        /// Set null to create a new user. Set user's Id to update a user
        /// </summary>
        public new long? Id { get; set; }

        //[Required]
        [StringLength(User.MaxNameLength)]
        public string Name
        {
            get => _name; set
            {
                _name = value;
                NotifyPropertyChange(() => Name);
            }

        }

        //[Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        //[Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(User.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        // Not used "Required" attribute since empty value is used to 'not change password'
        [StringLength(User.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get => _password; set { _password = value;
                NotifyPropertyChange(() => Password);
            } }

        public bool IsActive { get; set; }

        public bool ShouldChangePasswordOnNextLogin { get; set; }

        public virtual bool IsTwoFactorEnabled { get; set; }

        public virtual bool IsLockoutEnabled { get; set; }
    }
}
