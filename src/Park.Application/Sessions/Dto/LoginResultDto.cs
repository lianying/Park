using Abp.Application.Navigation;
using Park.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Sessions.Dto
{
    public class LoginResultDto
    {
        //public UserLoginInfoDto User { get; set; }

        public NgMenu Menu { get; set; }

        public AppConsts App { get; set; }

    }


    public class AppConsts {
        /// <summary>
        /// AppName
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string description { get; set; }


        public DateTime year { get; set; }
    }
}
