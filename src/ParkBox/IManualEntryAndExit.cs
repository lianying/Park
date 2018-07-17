using Park.Parks.Entrance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park
{
    interface IManualEntryAndExit
    {
        void ManualEntryAndExit(EntranceDto entranceDto);
    }
}
