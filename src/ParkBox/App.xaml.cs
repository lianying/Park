using Abp;
using Abp.Authorization;
using Abp.Runtime.Validation;
using Abp.UI;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Park.Froms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Park
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private readonly AbpBootstrapper _bootstrapper;
        private readonly ILogger _logger;
        private LoginWindow _loginWindow;

        private MainWindow _mainWindow;

        public App()
        {
            _bootstrapper = AbpBootstrapper.Create<ParkBoxWpfModule>();
            _bootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(x => x.UseLog4Net().WithConfig("log4net.config"));
            _logger = NullLogger.Instance;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        #region ApplicationException
        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ExceptioProcessor((Exception)e.Exception);

            e.Handled = true;
        }



        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            ExceptioProcessor(e.Exception);
            e.Handled = true;
        }
        private void ExceptioProcessor(Exception exception)
        {
            if (!exception.Data.Contains(exception.GetHashCode()))
            {
                exception.Data.Add(exception.GetHashCode(), null);
            }
            else
            {
                exception.Data.Remove(exception.GetHashCode());
                return;
            }
            if (exception is AbpAuthorizationException || exception is AbpValidationException || exception is UserFriendlyException)
            {
                MessageBox.Show(exception.Message, "提示");
            }
            else
            {
                _logger.Error("not AbpException", exception);
                MessageBox.Show("发生未知异常：" + exception.Message);
            }
        }

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            _bootstrapper.Initialize();

            _loginWindow = _bootstrapper.IocManager.Resolve<LoginWindow>();

            var isLogin = _loginWindow.ShowDialog();

            if (isLogin.HasValue && isLogin.Value)
            {

                _mainWindow = _bootstrapper.IocManager.Resolve<MainWindow>();
                _mainWindow.Show();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _bootstrapper.IocManager.Release(_loginWindow);
            _bootstrapper.Dispose();
        }

        
    }
}
