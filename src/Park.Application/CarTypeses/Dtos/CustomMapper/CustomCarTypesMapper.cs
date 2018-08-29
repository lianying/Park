

using AutoMapper;
using Park.CarTypeses;
using Park.Entitys.CarTypes;

namespace Park.CarTypeses.Dtos.CustomMapper
{

    /// <summary>
    /// 配置CarTypes的AutoMapper
    ///</summary>
    internal static class CustomerCarTypesMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CarTypes, CarTypesListDto>
    ();
            configuration.CreateMap<CarTypesEditDto, CarTypes>
                ();



            //// custom codes

            //// custom codes end

        }
    }
}
