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
        public const int DefaultPageSize = 100;
        /// <summary>
        /// 最大返回条数
        /// </summary>
        public const int MaxPageSize = 10000000;

        public const string AppName = "Park";

        public const string Description = "停车场管理系统";


        public const string TempCarInConfirmMessage = "临时车是否进入?";


        public const string NoNumberCarInConfirmMessage = "无牌车是否进入?";


        public const string NoPermissionNotInMessage = "禁止进入";

        public const string NoNumberNotInMessage = "无牌车禁止进入";


        public const string CarportsFullNotInMessage = "集团车位已满禁止进入";

        public const string TempCarNotInMessage = "临时车禁止进入";


        public static string[] ParkHeadColor = { "#8CC1B7",
            "#EC7E7F",
            "#91D065",
            "#F3B32B",
            "#73ACCA",
            "#A37FA5",
            "#8CC1B7"
        };


        public static string[] CarNumberList = { "浙A54265",
            "浙A54266",
            "浙A12345",
            "浙A12346",
            "浙A12347",
            "浙A12348",
            "浙A12349",
            "浙A12351",
            "浙A12352",
            "浙A12353",
            "浙A12354",
            "浙A12355",
            "浙A12356",
            "浙A12357" };


        public static string[] DeviceIds = {
            "00001",
            "00002",
            "00003",
            "00004",
            "00005"
        };


    }
}