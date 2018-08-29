

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.ParkEntrances;

namespace Park.ParkEntranceses.Dtos
{
    public class CreateOrUpdateParkEntrancesInput
    {
        [Required]
        public ParkEntrancesEditDto ParkEntrances { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}