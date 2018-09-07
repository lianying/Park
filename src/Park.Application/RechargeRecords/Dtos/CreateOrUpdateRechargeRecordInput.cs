

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.CarUsers;

namespace Park.RechargeRecords.Dtos
{
    public class CreateOrUpdateRechargeRecordInput
    {
        [Required]
        public RechargeRecordEditDto RechargeRecord { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}