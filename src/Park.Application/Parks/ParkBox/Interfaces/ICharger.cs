using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Park.Entitys.Box;

namespace Park.Parks.ParkBox.Interfaces
{
    public interface ICharger:ITransientDependency
    {
        bool? ShowDialog();


        void Show();

        void Init(bool isReset);


        CarOutRecord CarOutRecord { get; }


        CarOutModel CarOutModel { get; }
    }
}
