

using AutoMapper;
using Park.ParkEntrancePermissions;
using Park.Entitys.ParkEntrances;

namespace Park.ParkEntrancePermissions.Dtos.CustomMapper
{

	/// <summary>
    /// 配置ParkEntrancePermission的AutoMapper
    ///</summary>
	internal static class CustomerParkEntrancePermissionMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <ParkEntrancePermission, ParkEntrancePermissionListDto>
    ();
    configuration.CreateMap <ParkEntrancePermissionEditDto, ParkEntrancePermission>
        ();

            configuration.CreateMap<ParkEntrancePermissionListDto, ParkEntrancePermissionEditDto>();


            configuration.CreateMap<ParkEntrancePermissionListDto, ParkEntrancePermission>();



            //// custom codes

            //// custom codes end

        }
        }
        }
