using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Entitys.Box;

namespace Park.Parks.ParkBox
{
    public class IsCarInModel
    {
       public bool IsCarIn { get { return CarInRecord != null; } }


        public CarInRecord CarInRecord { get; set; }
    }
}
