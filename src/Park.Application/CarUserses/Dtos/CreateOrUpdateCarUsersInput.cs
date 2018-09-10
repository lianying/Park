

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.CarUsers;

namespace Park.CarUserses.Dtos
{
    public class CreateOrUpdateCarUsersInput
    {
        [Required]
        public CarUsersListDto CarUsers { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}