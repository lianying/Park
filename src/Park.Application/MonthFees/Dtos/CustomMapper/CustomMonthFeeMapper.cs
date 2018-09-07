

using AutoMapper;
using Park.MonthFees;
using Park.Entitys.FareRules;

namespace Park.MonthFees.Dtos.CustomMapper
{

	/// <summary>
    /// 配置MonthFee的AutoMapper
    ///</summary>
	internal static class CustomerMonthFeeMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <MonthFee, MonthFeeListDto>
    ();
    configuration.CreateMap <MonthFeeEditDto, MonthFee>
        ();

            configuration.CreateMap<MonthFeeListDto, MonthFeeEditDto>();



        //// custom codes
         
        //// custom codes end

        }
        }
        }
