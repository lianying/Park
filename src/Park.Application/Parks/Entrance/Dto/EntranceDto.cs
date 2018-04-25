using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Park.Entitys.ParkEntrances;
using Park.Enum;
using Park.Parks.Level.Dto;

namespace Park.Parks.Entrance
{
    [AutoMap(typeof(ParkEntrances))]
    public class EntranceDto:EntityDto<long>
    {
        public ParkLevelDto ParkLevel { get; set; }

        public EntranceType EntranceType { get; set; }

        public string EntranceName { get; set; }



    }
}
