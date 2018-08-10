using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Park.DeviceSDK.LanKa
{
	class JsonUtility {  /// <summary>
		/// 对象转换成Json
		/// </summary>
		/// <param name="obj">对象实例</param>
		/// <returns></returns>
		public static string ObjectToJson(object obj) {
			try {
				//return JinQuAPI.RequestHelper.ObjToJson(obj);
				JavaScriptSerializer js = new JavaScriptSerializer();
				return js.Serialize(obj);
			}
			catch (Exception ex) {
				return string.Empty;
			}
		}

		/// <summary>
		/// Json转换成对象

		/// </summary>
		/// <param name="strJson">Json字符串</param>
		/// <param name="type">对象类型</param>
		/// <returns></returns>
		public static object JsonToObject(string strJson, Type type) {
			try {
				JavaScriptSerializer js = new JavaScriptSerializer();
				return js.Deserialize(strJson, type);
			}
			catch (Exception ex) {
				// Log.WriterError("JsonToObject:" + ex.Message);
				return new object();
			}
		}

		/// <summary>
		/// 对象转换成Json
		/// </summary>
		/// <param name="obj">对象实例</param>
		/// <returns></returns>
		public static string DictionaryToJson(Dictionary<string, object> dic) {
			try {
				if (dic == null || dic.Count <= 0) {
					return string.Empty;
				}
				StringBuilder str = new StringBuilder();
				str.Append("{");
				foreach (string key in dic.Keys) {
					if (dic[key].GetType() == typeof(System.String)) {
						str.Append("\"" + key + "\"" + ":" + "\"" + dic[key].ToString() + "\"" + ",");
					}
					else if (dic[key].GetType() == typeof(System.Int32) || dic[key].GetType() == typeof(System.Decimal) ||
							 dic[key].GetType() == typeof(System.Double) || dic[key].GetType() == typeof(System.Single)) {
						str.Append("\"" + key + "\"" + ":" + dic[key].ToString() + ",");
					}
				}
				str.Remove(str.Length - 1, 1);
				str.Append("}");
				return str.ToString();
			}
			catch (Exception ex) {
				// Log.WriterError("DictionaryToJson:" + ex.Message);
				return string.Empty;
			}
		}

		/// <summary>
		/// JSON转成Dictionary
		/// </summary>
		/// <param name="strJson"></param>
		/// <returns></returns>
		public static Dictionary<string, string> ConvertJsonToDictionary(string strJson, ref string errorcode) {
			try {
				JavaScriptSerializer js = new JavaScriptSerializer();
				return js.Deserialize(strJson, typeof(Dictionary<string, object>)) as Dictionary<string, string>;
			}
			catch (Exception ex) {
				errorcode = "100100";
				// Log.WriterError("ConvertJsonToDictionary:" + ex.Message);
				return new Dictionary<string, string>();
			}
		}


		/// <summary>
		/// JSON转成Dictionary
		/// </summary>
		/// <param name="strJson"></param>
		/// <returns></returns>
		public static Dictionary<string, string> ConvertJsonToDictionary(string strJson) {
			try {
				JavaScriptSerializer js = new JavaScriptSerializer();
				return js.Deserialize(strJson, typeof(Dictionary<string, object>)) as Dictionary<string, string>;
			}
			catch (Exception ex) {
				// Log.WriterError("ConvertJsonToDictionary:" + ex.Message);
				return new Dictionary<string, string>();
			}
		}
	}
}
