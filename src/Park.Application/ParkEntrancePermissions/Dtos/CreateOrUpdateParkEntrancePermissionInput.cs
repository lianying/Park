

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.ParkEntrances;

namespace Park.ParkEntrancePermissions.Dtos
{
    public class CreateOrUpdateParkEntrancePermissionInput
    {
        [Required]
        public ParkEntrancePermissionEditDto ParkEntrancePermission { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}