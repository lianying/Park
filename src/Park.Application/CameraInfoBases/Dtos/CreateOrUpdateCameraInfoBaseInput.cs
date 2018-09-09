

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys;

namespace Park.CameraInfoBases.Dtos
{
    public class CreateOrUpdateCameraInfoBaseInput
    {
        [Required]
        public CameraInfoBaseEditDto CameraInfoBase { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}