

using Abp.Runtime.Validation;
using Park.Dtos;
using Park.Entitys.CarUsers;

namespace Park.CarPorts.Dtos
{
    public class GetCarPortsInput : PagedAndSortedInputDto, IShouldNormalize
    {
          /// <summary>
		 /// 模糊搜索使用的关键字
		 ///</summary>
        public string Filter { get; set; }


		//// custom codes
 
        //// custom codes end

			  /// <summary>
			 /// 正常化排序使用
			///</summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

        public int ParkId { get; set; }


        public long? AreaId { get; set; }

        public long? CarTypeId { get; set; }


    }
}
