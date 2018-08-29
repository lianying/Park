

using AutoMapper;
using Park.ParkEntranceses;
using Park.Entitys.ParkEntrances;

namespace Park.ParkEntranceses.Dtos.CustomMapper
{

	/// <summary>
    /// 配置ParkEntrances的AutoMapper
    ///</summary>
	internal static class CustomerParkEntrancesMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ParkEntrances, ParkEntrancesListDto>
    ();
    configuration.CreateMap <ParkEntrancesEditDto, ParkEntrances>
        ();



        //// custom codes
         
        //// custom codes end

        }
        }
        }
