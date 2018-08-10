using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Park.DeviceSDK.LanKa
{
    class Ini
    {

        [DllImport("JsonDl.dll", EntryPoint = "GetJsonValue", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]     // "JsonDl.dll"为dll名称,EntryPoint 为函数名
        public static extern IntPtr GetJsonValue(string strFiled, string strjson);

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath); 
        /// <summary>
        ///  读INI文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadIni(string section, string key, string filePath)
        {
            StringBuilder str = new StringBuilder(500);
            GetPrivateProfileString(section, key, "", str, 255, filePath);
            return str.ToString();
        }
        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static long WriteIni(string section, string key, string value, string filePath)
        {
            return WritePrivateProfileString(section, key, value, filePath);
        }
        public static string[] ReadAllKeys(string section, string filePath)
        {
            byte[] temp = new byte[1024];
            int i = GetPrivateProfileString(section, null, "", temp, 1024, filePath);
            return System.Text.Encoding.UTF8.GetString(temp).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
