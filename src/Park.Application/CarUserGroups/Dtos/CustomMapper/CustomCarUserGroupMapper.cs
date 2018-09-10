

using AutoMapper;
using Park.CarUserGroups;
using Park.Entitys.CarUsers;

namespace Park.CarUserGroups.Dtos.CustomMapper
{

	/// <summary>
    /// 配置CarUserGroup的AutoMapper
    ///</summary>
	internal static class CustomerCarUserGroupMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <CarUserGroup, CarUserGroupListDto>
    ();
    configuration.CreateMap <CarUserGroupEditDto, CarUserGroup>
        ();


            configuration.CreateMap<CarUserGroupListDto, CarUserGroup>
                ();



            //// custom codes

            //// custom codes end

        }
        }
        }
