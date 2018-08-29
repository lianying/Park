

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.CarUsers;

namespace Park.CarNumberses.Dtos
{
    public class CreateOrUpdateCarNumbersInput
    {
        [Required]
        public CarNumbersEditDto CarNumbers { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}