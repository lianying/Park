namespace Park
{
    public class ParkConsts
    {
        public const string LocalizationSourceName = "Park";

        public const bool MultiTenancyEnabled = true;

        public const int MaxCloudIdLength = 285;
        /// <summary>
        /// 默认返回条数
        /// </summary>
        public const int DefultPageSize = 100;
        /// <summary>
        /// 最大返回条数
        /// </summary>
        public const int MaxPageSize = 10000;

        public const string AppName = "Park";

        public const string Description = "停车场管理系统";


       public const string TempCarInConfirmMessage = "临时车是否进入?";


        public const string NoNumberCarInConfirmMessage = "无牌车是否进入?";


        public const string NoPermissionNotInMessage = "禁止进入";

        public const string NoNumberNotInMessage = "无牌车禁止进入";


        public const string CarportsFullNotInMessage = "集团车位已满禁止进入";

        public const string TempCarNotInMessage = "临时车禁止进入";


    }
}