

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.CarUsers;

namespace Park.CarPorts.Dtos
{
    public class CreateOrUpdateCarPortInput
    {
        [Required]
        public CarPortEditDto CarPort { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}