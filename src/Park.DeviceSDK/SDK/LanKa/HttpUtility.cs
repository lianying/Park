using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Park.DeviceSDK.LanKa
{
    class HttpUtility
    {
        
        /// <summary>
        /// 发送请求

        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="data">数据</param>
        public static string SendHttpRequest(string url, string data)
        {
            try
            {
                return SendPostHttpRequest(url, "application/x-www-form-urlencoded", data);
            }
            catch (Exception ex)
            {
                WriteLog.EventLog("SendHttpRequest:" + ex.Message);
                return "请求异常！";
            }
        }
        /// <summary>
        /// 核销优惠券请求

        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string SendHttpForDiscount(string url, string data)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    WriteLog.EventLog("SendHttpForDiscount:URL为空，不发送请求");
                    return string.Empty;
                }
                else
                    return SendPostHttpRequest(url, "application/json", data);

            }
            catch (Exception ex)
            {
                WriteLog.EventLog("SendHttpRequest:" + ex.Message);
                return "请求异常！";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetData(string url)
        {
            try
            {
                return SendGetHttpRequest(url, "application/x-www-form-urlencoded");
            }
            catch (Exception ex)
            {
                WriteLog.EventLog("GetData:" + ex.Message);
                return "请求异常！";
            }
        }

        /// <summary>
        /// 发送请求

        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="method">方法（post或get）</param>
        /// <param name="method">数据类型</param>
        /// <param name="requestData">数据</param>
        public static string SendPostHttpRequest(string url, string contentType, string requestData)
        {
            try
            {
                WriteLog.EventLog("SendPostHttpRequest URL=" + url + ",Content=" + requestData+ "http");
                WebRequest request = (WebRequest)HttpWebRequest.Create(url);
                request.Method = "POST";
                byte[] postBytes = null;
                request.ContentType = contentType;
                postBytes = Encoding.UTF8.GetBytes(requestData);
                request.ContentLength = postBytes.Length;
                request.Timeout = 3 * 1000;
                using (Stream outstream = request.GetRequestStream())
                {
                    outstream.Write(postBytes, 0, postBytes.Length);
                }
                string result = string.Empty;
                using (WebResponse response = request.GetResponse())
                {
                    if (response != null)
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                result = reader.ReadToEnd();
                            }
                        }

                    }
                }
                WriteLog.EventLog("SendPostHttpRequest Result=" + result + "http");
                return result;
            }
            catch (Exception ex)
            {
                WriteLog.EventLog("SendPostHttpRequest:" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 发送请求

        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="method">方法（post或get）</param>
        /// <param name="method">数据类型</param>
        /// <param name="requestData">数据</param>
        public static string SendPostHttpRequestForMedia(string url, string requestData)
        {
            try
            {
                WebRequest request = (WebRequest)HttpWebRequest.Create(url);
                request.Method = "POST";
                byte[] postBytes = null;
                request.ContentType = "application / x - www - form - urlencoded";
                postBytes = Encoding.UTF8.GetBytes(requestData);
                request.ContentLength = postBytes.Length;
                using (Stream outstream = request.GetRequestStream())
                {
                    outstream.Write(postBytes, 0, postBytes.Length);
                }
                string result = string.Empty;
                using (WebResponse response = request.GetResponse())
                {
                    if (response != null)
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                result = reader.ReadToEnd();
                            }
                        }

                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                WriteLog.EventLog("SendPostHttpRequestForMedia:" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 发送请求

        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="method">方法（post或get）</param>
        /// <param name="method">数据类型</param>
        /// <param name="requestData">数据</param>
        public static string SendGetHttpRequest(string url, string contentType)
        {
            try
            {
                WebRequest request = (WebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = contentType;
                string result = string.Empty;
                using (WebResponse response = request.GetResponse())
                {
                    if (response != null)
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                result = reader.ReadToEnd();
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                WriteLog.EventLog("SendGetHttpRequest:" + ex.Message);
                return string.Empty;
            }
        }
    }
}
