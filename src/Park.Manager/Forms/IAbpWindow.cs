using Abp.Application.Features;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Castle.Core.Logging;

namespace Park.Froms
{
    interface IAbpWindow
    {
        IAbpSession AbpSession { get; set; }

        ILogger Logger { get; set; }

        IObjectMapper ObjectMapper { get; set; }

        IUnitOfWorkManager UnitOfWorkManager { get; set; }

        IEventBus EventBus { get; set; }

        IPermissionManager PermissionManager { get; set; }
        IPermissionChecker PermissionChecker { get; set; }

        IActiveUnitOfWork ActiveUnitOfWork { get; }

        ILocalizationSource LocalizationSource { get; }

        ILocalizationManager LocalizationManager { get; set; }

        IFeatureChecker FeatureChecker { get; set; }

        IFeatureManager FeatureManager { get; set; }

        ISettingManager SettingManager { get; set; }


        string LocalizationSourceName { get; set; }





    }
}
