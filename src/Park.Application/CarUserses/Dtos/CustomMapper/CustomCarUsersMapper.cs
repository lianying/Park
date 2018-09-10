

using AutoMapper;
using Park.CarUserses;
using Park.Entitys.CarUsers;

namespace Park.CarUserses.Dtos.CustomMapper
{

	/// <summary>
    /// 配置CarUsers的AutoMapper
    ///</summary>
	internal static class CustomerCarUsersMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <CarUsers, CarUsersListDto>
    ();
    configuration.CreateMap <CarUsersEditDto, CarUsers>
        ();

            configuration.CreateMap<CarUsersListDto, CarUsers>();


        //// custom codes
         
        //// custom codes end

        }
        }
        }
