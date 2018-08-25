

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.ParkAreas;

namespace Park.ParkAreases.Dtos
{
    public class CreateOrUpdateParkAreasInput
    {
        [Required]
        public ParkAreasEditDto ParkAreas { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}