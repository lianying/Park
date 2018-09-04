

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.CarUsers;

namespace Park.CarUserGroups.Dtos
{
    public class CreateOrUpdateCarUserGroupInput
    {
        [Required]
        public CarUserGroupEditDto CarUserGroup { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}