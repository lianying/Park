

using AutoMapper;
using Park.CarPorts;
using Park.Entitys.CarUsers;

namespace Park.CarPorts.Dtos.CustomMapper
{

	/// <summary>
    /// 配置CarPort的AutoMapper
    ///</summary>
	internal static class CustomerCarPortMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <CarPort, CarPortListDto>
    ();
    configuration.CreateMap <CarPortEditDto, CarPort>
        ();



        //// custom codes
         
        //// custom codes end

        }
        }
        }
