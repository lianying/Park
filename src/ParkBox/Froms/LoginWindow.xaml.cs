﻿using Abp.Authorization;
using Abp.Dependency;
using Abp.UI;
using Park.Authorization;
using Park.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Park.Froms
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : AbpWindow, ISingletonDependency
    {
        private readonly LogInManager _logInManager;

        private readonly ISessionAppService _sessionAppService;
        public LoginWindow(LogInManager logInManager, ISessionAppService sessionAppService)
        {
            _logInManager = logInManager;
            _sessionAppService = sessionAppService;
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = "admin";
            string pwd = "123qwe";
            var loginResult = await _logInManager.LoginAsync(userName, pwd);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    Thread.CurrentPrincipal = new ClaimsPrincipal(loginResult.Identity);
                    var res= await Task.Run(async () => await _sessionAppService.GetCurrentWebInfo());
                    break;
                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, userName, "");
                    break;
            }

            
        }

        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), "Your email address is not confirmed. You can not login"); //TODO: localize message
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }
    }
}
