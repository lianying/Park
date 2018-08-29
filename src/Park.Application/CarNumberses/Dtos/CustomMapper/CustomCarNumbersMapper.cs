

using AutoMapper;
using Park.CarNumberses;
using Park.Entitys.CarUsers;

namespace Park.CarNumberses.Dtos.CustomMapper
{

	/// <summary>
    /// 配置CarNumbers的AutoMapper
    ///</summary>
	internal static class CustomerCarNumbersMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <CarNumbers, CarNumbersListDto>
    ();
    configuration.CreateMap <CarNumbersEditDto, CarNumbers>
        ();



        //// custom codes
         
        //// custom codes end

        }
        }
        }
