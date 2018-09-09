

using AutoMapper;
using Park.CameraInfoBases;
using Park.Entitys;

namespace Park.CameraInfoBases.Dtos.CustomMapper
{

	/// <summary>
    /// 配置CameraInfoBase的AutoMapper
    ///</summary>
	internal static class CustomerCameraInfoBaseMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <CameraInfoBase, CameraInfoBaseListDto>
    ();
    configuration.CreateMap <CameraInfoBaseEditDto, CameraInfoBase>
        ();



        //// custom codes
         
        //// custom codes end

        }
        }
        }
