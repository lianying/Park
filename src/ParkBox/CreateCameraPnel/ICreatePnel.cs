using Park.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Abp.Dependency;

namespace Park.CreateCameraPnel
{
    public interface ICreatePnel : ITransientDependency
    {
        Dictionary<long, ParkEntranceInfo> CreatePnels(Control window);
    }
}
