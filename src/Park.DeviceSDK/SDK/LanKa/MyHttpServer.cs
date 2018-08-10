using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Park.DeviceSDK.LanKa
{
	//class MyHttpServer:IDisposable
	//{
	//	public  delegate void Newrecord(string str);
	//	public  event Newrecord EventNewrecord;

	//	public  HttpListener listener;
	//	public bool isClose = true;
	//	public  void StartListen(string BasicUrl)
	//	{
	//		listener = new HttpListener();
	//		listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
	//		listener.Prefixes.Add(BasicUrl);
	//		listener.Start();
	//		Thread td = new Thread(ProcessHttp);
	//		td.IsBackground = true;
	//		td.Start(listener);

	//	}

	//	private void ProcessHttp(object lstn)
	//	{
	//		HttpListener listener = (HttpListener)lstn;
	//		while (isClose)
	//		{
	//			HttpListenerContext ctx = listener.GetContext();
	//			ThreadPool.QueueUserWorkItem(new WaitCallback(TaskProc), ctx);

	//		}
	//	}

	//	private string ProcessInfo(string strRawUrl, string strFileName,string text)
	//	{
	//		string strBack = "{\"mess\":\"ok\",\"code\":\"201\"}";
	//		try
	//		{
	//			if (strRawUrl.IndexOf("PlateandImg") >= 0)
	//			{
	//				EventNewrecord(text);
	//			}

	//		}
	//		catch (Exception ex)
	//		{
	//			//  Common.LogUtility.WriteError(typeof(MyHttpServer), "ProcessInfo:" + ex.Message);
	//		}

	//		return strBack;
	//	}

	//	private void TaskProc(Object o)
	//	{
	//		try
	//		{
	//			HttpListenerContext ctx = (HttpListenerContext)o;
	//			ctx.Response.StatusCode = 200;
	//			ctx.Response.ContentType = "application/json";
	//			ctx.Response.ContentEncoding = Encoding.UTF8;
	//			//    LogUtility.Write(typeof(MyHttpServer), "收到信息：" + ctx.Request.RawUrl);

	//			string strFileName = Path.GetFileName(ctx.Request.RawUrl);
	//			if (ctx.Request.RawUrl.IndexOf("?") >= 0)
	//				strFileName = ctx.Request.RawUrl.Substring(ctx.Request.RawUrl.IndexOf("?"));
	//			string strRequest = ShowRequestData(ctx.Request);
	//			string strBack = ProcessInfo(ctx.Request.RawUrl, strFileName,strRequest);


	//			//payment_time=2016-09-27%2009:32:17&license_plate=粤B12345&payment_money=1&payment_type=支付宝 -->
	//			//  string strRequest = ShowRequestData(ctx.Request);
	//			using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream,Encoding.UTF8))
	//			{
	//				writer.Write(strBack);
	//				writer.Close();
	//				ctx.Response.Close();
	//			}
	//			JinQuLogs.LogHelper.Info("回复内容：" + strBack + "main");

	//		}
	//		catch (Exception ex)
	//		{
	//		   JinQuLogs.LogHelper.Error("TaskProc:" + ex.Message);
	//		}
	//	}



	//	public static string ShowRequestData(HttpListenerRequest request)
	//	{
	//		try
	//		{
	//			if (!request.HasEntityBody)
	//			{
	//				return "";
	//			}
	//			System.IO.Stream body = request.InputStream;

	//			System.IO.StreamReader reader = new System.IO.StreamReader(body, Encoding.UTF8);
	//			string s = reader.ReadToEnd();
	//			string str = System.Web.HttpUtility.UrlDecode(s);

	//			body.Close();
	//			reader.Close();
	//			return str ;
	//		}
	//		catch (Exception ex)
	//		{
	//			JinQuLogs.LogHelper.Error("ShowRequestData:" + ex.Message);
	//		}
	//		return "";
	//	}

	//	public static void AddAddress(string address)
	//	{
	//		try
	//		{
	//			AddAddress(address, Environment.UserDomainName, Environment.UserName);
	//		}
	//		catch (Exception ex)
	//		{
	//			JinQuLogs.LogHelper.Error("AddAddress:" + ex.Message);
	//		}
	//	}



	//	public static void AddAddress(string address, string domain, string user)
	//	{
	//		try
	//		{
	//			string argsDll = String.Format(@"http delete urlacl url={0}", address);
	//			string args = string.Format(@"http add urlacl url={0} user={1}\{2}", address, domain, user);
	//			ProcessStartInfo psi = new ProcessStartInfo("netsh", argsDll);
	//			psi.Verb = "runas";
	//			psi.CreateNoWindow = true;
	//			psi.WindowStyle = ProcessWindowStyle.Hidden;
	//			psi.UseShellExecute = false;
	//			Process.Start(psi).WaitForExit();//删除urlacl
	//			psi = new ProcessStartInfo("netsh", args);
	//			psi.Verb = "runas";
	//			psi.CreateNoWindow = true;
	//			psi.WindowStyle = ProcessWindowStyle.Hidden;
	//			psi.UseShellExecute = false;
	//			Process.Start(psi).WaitForExit();//添加urlacl
	//		}
	//		catch (Exception ex)
	//		{
	//			JinQuLogs.LogHelper.Error("AddAddress:" + ex.Message);
	//		}

	//	}

	//	public void Dispose() {
	//		this.isClose = false;
	//		listener.Stop();
	//		listener = null;
	//	}
	//}
	public class MyHttpServer {
		public delegate void Newrecord(string str);
		public static event Newrecord EventNewrecord;
		public static bool isEventBind = false;

		public static HttpListener listener;
		public static void StartListen(string BasicUrl) {
			if (listener != null)
				return;
			BasicUrl = BasicUrl.EndsWith("/") ? BasicUrl : BasicUrl + "/";
			listener = new HttpListener();
			listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
			listener.Prefixes.Add(BasicUrl);
			listener.Start();
			Thread td = new Thread(ProcessHttp);
			td.IsBackground = true;
			td.Start(listener);

		}

		private static void ProcessHttp(object lstn) {
			HttpListener listener = (HttpListener)lstn;
			while (true) {
				HttpListenerContext ctx = listener.GetContext();
				ThreadPool.QueueUserWorkItem(new WaitCallback(TaskProc), ctx);

			}
		}

		public static void CheckLister(string text) {
			EventNewrecord(text);
		}

		private static string ProcessInfo(string strRawUrl, string strFileName, string text) {
			string strBack = "{\"mess\":\"ok\",\"code\":\"201\"}";
			try {
				if (strRawUrl.IndexOf("PlateandImg") >= 0) {
					EventNewrecord(text);
				}

			}
			catch (Exception ex) {
				//  Common.LogUtility.WriteError(typeof(MyHttpServer), "ProcessInfo:" + ex.Message);
			}

			return strBack;
		}

		private static void TaskProc(Object o) {
			try {
				HttpListenerContext ctx = (HttpListenerContext)o;
				ctx.Response.StatusCode = 200;
				ctx.Response.ContentType = "application/json";
				ctx.Response.ContentEncoding = Encoding.UTF8;
				//    LogUtility.Write(typeof(MyHttpServer), "收到信息：" + ctx.Request.RawUrl);

				string strFileName = Path.GetFileName(ctx.Request.RawUrl);
				if (ctx.Request.RawUrl.IndexOf("?") >= 0)
					strFileName = ctx.Request.RawUrl.Substring(ctx.Request.RawUrl.IndexOf("?"));
				string strRequest = ShowRequestData(ctx.Request);
				string strBack = ProcessInfo(ctx.Request.RawUrl, strFileName, strRequest);


				//payment_time=2016-09-27%2009:32:17&license_plate=粤B12345&payment_money=1&payment_type=支付宝 -->
				//  string strRequest = ShowRequestData(ctx.Request);
				using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream)) {
					writer.Write(strBack);
					writer.Close();
					ctx.Response.Close();
				}
                WriteLog.EventLog("回复内容：" + strBack + "main");

			}
			catch (Exception ex) {
                WriteLog.EventLog("TaskProc:" + ex.Message);
			}
		}



		public static string ShowRequestData(HttpListenerRequest request) {
			try {
				if (!request.HasEntityBody) {
					return "";
				}
				System.IO.Stream body = request.InputStream;

				System.IO.StreamReader reader = new System.IO.StreamReader(body, Encoding.UTF8);
				string s = reader.ReadToEnd();
				string str = System.Web.HttpUtility.UrlDecode(s);

				body.Close();
				reader.Close();
				return str;
			}
			catch (Exception ex) {
                WriteLog.EventLog("ShowRequestData:" + ex.Message);
			}
			return "";
		}

		public static void AddAddress(string address) {
			try {
				AddAddress(address, Environment.UserDomainName, Environment.UserName);
			}
			catch (Exception ex) {
                WriteLog.EventLog("AddAddress:" + ex.Message);
			}
		}



		public static void AddAddress(string address, string domain, string user) {
			try {
				string argsDll = String.Format(@"http delete urlacl url={0}", address);
				string args = string.Format(@"http add urlacl url={0} user={1}\{2}", address, domain, user);
				ProcessStartInfo psi = new ProcessStartInfo("netsh", argsDll);
				psi.Verb = "runas";
				psi.CreateNoWindow = true;
				psi.WindowStyle = ProcessWindowStyle.Hidden;
				psi.UseShellExecute = false;
				Process.Start(psi).WaitForExit();//删除urlacl
				psi = new ProcessStartInfo("netsh", args);
				psi.Verb = "runas";
				psi.CreateNoWindow = true;
				psi.WindowStyle = ProcessWindowStyle.Hidden;
				psi.UseShellExecute = false;
				Process.Start(psi).WaitForExit();//添加urlacl
			}
			catch (Exception ex) {
				WriteLog.EventLog("AddAddress:" + ex.Message);
			}

		}
	}
}
