

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.CarTypes;

namespace Park.CarTypeses.Dtos
{
    public class CreateOrUpdateCarTypesInput
    {
        [Required]
        public CarTypesEditDto CarTypes { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}