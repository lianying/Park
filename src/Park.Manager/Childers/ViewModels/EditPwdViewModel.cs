using Park.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Childers.ViewModels
{
    public class EditPwdViewModel : NotifyPropertyChangeBase
    {
        private string _oldPwd;
        private string _newPwd;
        private string _sureNewPwd;

        public string OldPwd
        {
            get => _oldPwd; set
            {
                _oldPwd = value;
                NotifyPropertyChange(() => OldPwd);
            }
        }

        public string NewPwd
        {
            get => _newPwd; set
            {
                _newPwd = value;
                NotifyPropertyChange(() => NewPwd);
            }
        }

        public string SureNewPwd
        {
            get => _sureNewPwd; set
            {
                _sureNewPwd = value;
                NotifyPropertyChange(() => SureNewPwd);
            }
        }
    }
}
