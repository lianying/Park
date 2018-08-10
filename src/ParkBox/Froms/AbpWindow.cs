using Abp;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using MahApps.Metro.Controls;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Park.Froms
{
    public class AbpWindow : MetroWindow, IAbpWindow
    {

        

        public IAbpSession AbpSession { get; set; }
        public ILogger Logger { get; set; }
        public IObjectMapper ObjectMapper { get; set; }
        public IUnitOfWorkManager UnitOfWorkManager { get; set; }
        public IEventBus EventBus { get; set; }
        public IPermissionManager PermissionManager { get; set; }
        public IPermissionChecker PermissionChecker { get; set; }

        public SynchronizationContext SynchronizationContext { get; private set; }



        public IActiveUnitOfWork ActiveUnitOfWork => UnitOfWorkManager.Current;

        public ILocalizationSource LocalizationSource
        {
            get
            {

                if (LocalizationSourceName == null)
                    throw new AbpException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }
                return _localizationSource;
            }
        }

        public ILocalizationManager LocalizationManager { get; set; }
        public IFeatureChecker FeatureChecker { get; set; }
        public IFeatureManager FeatureManager { get; set; }
        public ISettingManager SettingManager { get; set; }

        public string LocalizationSourceName { get; set; }

        public IocManager IocManager { get; set; }


        private ILocalizationSource _localizationSource;

        public AbpWindow()
        {
            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
            EventBus = NullEventBus.Instance;
            ObjectMapper = NullObjectMapper.Instance;

            LocalizationSourceName = ParkConsts.LocalizationSourceName;

            SynchronizationContext = DispatcherSynchronizationContext.Current;
            IocManager = IocManager.Instance;

        }

        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }


        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }


        protected Task<bool> IsGrantedAsync(string permissionName)
        {
            return PermissionChecker.IsGrantedAsync(permissionName);
        }


        protected bool IsGranted(string permissionName)
        {
            return PermissionChecker.IsGranted(permissionName);
        }

        protected virtual Task<bool> IsEnabledAsync(string featureName)
        {
            return FeatureChecker.IsEnabledAsync(featureName);
        }

        protected virtual bool IsEnabled(string featureName)
        {
            return FeatureChecker.IsEnabled(featureName);
        }







    }
}
