using Park.Entitys.Box;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Park.Parks.ParkBox.Interfaces
{
    public interface INotify
    {


        void Message(string titi, string message);
    }


    public interface ISetInfo:ISingletonDependency
    {
        Task SetInfo( CarInRecord carInRecord);


        Task SetInfo(CarOutRecord carOutRecord);


        Task SetImage( Stream stream);

        Task OpenRod();


    }
}
