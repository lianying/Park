

using AutoMapper;
using Park.RechargeRecords;
using Park.Entitys.CarUsers;

namespace Park.RechargeRecords.Dtos.CustomMapper
{

	/// <summary>
    /// 配置RechargeRecord的AutoMapper
    ///</summary>
	internal static class CustomerRechargeRecordMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <RechargeRecord, RechargeRecordListDto>
    ();
    configuration.CreateMap <RechargeRecordEditDto, RechargeRecord>
        ();



        //// custom codes
         
        //// custom codes end

        }
        }
        }
