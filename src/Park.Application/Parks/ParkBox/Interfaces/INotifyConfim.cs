using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.ParkBox.Interfaces
{
    public interface INotifyConfim
    {
        bool Confirm(string title, string message);


        void Message(string titi, string message);
    }
}
