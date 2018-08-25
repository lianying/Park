

using AutoMapper;
using Park.ParkAreases;
using Park.Entitys.ParkAreas;

namespace Park.ParkAreases.Dtos.CustomMapper
{

	/// <summary>
    /// 配置ParkAreas的AutoMapper
    ///</summary>
	internal static class CustomerParkAreasMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ParkAreas, ParkAreasListDto>
    ();
    configuration.CreateMap <ParkAreasEditDto, ParkAreas>
        ();



        //// custom codes
         
        //// custom codes end

        }
        }
        }
