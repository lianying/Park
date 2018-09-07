

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Park.Entitys.FareRules;

namespace Park.MonthFees.Dtos
{
    public class CreateOrUpdateMonthFeeInput
    {
        [Required]
        public MonthFeeEditDto MonthFee { get; set; }



		//// custom codes
 
        //// custom codes end
    }
}