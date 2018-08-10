using Park.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Abp.Dependency;
using Park.Parks.ParkBox.Interfaces;

namespace Park.CreateCameraPnel
{
    public interface ICreatePnel : ITransientDependency
    {
        Dictionary<long, ISetInfo> CreatePnels(Control window);
    }
}
