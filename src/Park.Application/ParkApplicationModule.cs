using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using Park.Authorization.Roles;
using Park.Authorization.Users;
using Park.CarNumberses.Dtos.CustomMapper;
using Park.CarPorts.Dtos.CustomMapper;
using Park.CarUserGroups.Dtos.CustomMapper;
using Park.CarUserses.Dtos.CustomMapper;
using Park.MonthFees.Dtos.CustomMapper;
using Park.ParkEntrancePermissions.Dtos.CustomMapper;
using Park.ParkEntranceses.Dtos.CustomMapper;
using Park.Roles.Dto;
using Park.Users.Dto;

namespace Park
{
    [DependsOn(typeof(ParkCoreModule), typeof(AbpAutoMapperModule))]
    public class ParkApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Configuration.Auditing.IsEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // TODO: Is there somewhere else to store these, with the dto classes
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Role and permission
                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                CarTypeses.Dtos.CustomMapper.CustomerCarTypesMapper.CreateMappings(cfg);

                CustomerParkEntrancesMapper.CreateMappings(cfg);
                CustomerParkEntrancePermissionMapper.CreateMappings(cfg);
                CustomerCarUserGroupMapper.CreateMappings(cfg);
                CustomerCarUsersMapper.CreateMappings(cfg);
                CustomerCarNumbersMapper.CreateMappings(cfg);
                CustomerCarPortMapper.CreateMappings(cfg);
                CustomerMonthFeeMapper.CreateMappings(cfg);

            });
        }
    }
}
