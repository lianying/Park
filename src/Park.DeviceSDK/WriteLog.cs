using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Park.DeviceSDK
{
    class WriteLog
    {
        #region 多线程调用的写日志，不会有临界问题时用


        public static void Log_Notime(string WriteLog, string fileName)
        {
            if (fileName.Equals("")) return;
            string strPath = "";//按当前天来记录日志

            string dir = "";
            dir = AppDomain.CurrentDomain.BaseDirectory + "\\log\\" + DateTime.Now.Date.ToString("yyyyMMdd") + "\\" + fileName;
            try
            {
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                strPath = dir + "\\" + DateTime.Now.ToString("HH") + ".log";
                if (!File.Exists(strPath))
                {
                    using (FileStream fs = File.Create(strPath))
                    {
                        fs.Dispose();
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode))
                {

                    sw.Write(WriteLog + Environment.NewLine);
                    sw.Flush();
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch
            {

            }
        }
        #endregion

        #region
        public static void Log(string WriteLog, string fileName)
        {
            if (fileName.Equals("")) return;
            string strPath = "";//按当前天来记录日志

            string dir = "";
            dir = AppDomain.CurrentDomain.BaseDirectory + "\\log\\" + DateTime.Now.Date.ToString("yyyyMMdd") + "\\" + fileName;
            try
            {
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                strPath = dir + "\\" + DateTime.Now.ToString("HH") + ".log";
                if (!File.Exists(strPath))
                {
                    using (FileStream fs = File.Create(strPath))
                    {
                        fs.Dispose();
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode))
                {

                    sw.Write(DateTime.Now.ToString("HH:mm:ss fff") + "-" + WriteLog + Environment.NewLine);
                    sw.Flush();
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch
            {

            }
        }
        #endregion

        public static void TimeLog(string WriteLog, string fileName)
        {
            if (fileName.Equals("")) return;
            string strPath = "";//按当前天来记录日志

            string dir = "";
            dir = AppDomain.CurrentDomain.BaseDirectory + "\\log\\" + DateTime.Now.Date.ToString("yyyyMMdd") + "\\" + fileName;
            try
            {
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                strPath = dir + "\\" + fileName + ".log";
                if (!File.Exists(strPath))
                {
                    using (FileStream fs = File.Create(strPath))
                    {
                        fs.Dispose();
                        fs.Close();
                    }
                }
                using (StreamWriter sw = new StreamWriter(strPath, true, Encoding.Unicode))
                {
                    sw.Write(WriteLog + Environment.NewLine);
                    sw.Flush();
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch
            {

            }
        }

        #region
        public static void ByteLog(byte[] bytes, string fileName)
        {
            // string str  = "";              
            try
            {

            }
            catch
            {

            }
        }
        #endregion

        #region 数据库异常报错时调用
        public static object Eventlocker = new object();
        public static void EventLog(string WriteLog)  //数据库异常
        {
            lock (Eventlocker)
            {
                try
                {
                    Log(WriteLog, "Event");
                }
                catch
                {

                }
            }
        }
        #endregion

        #region 有临界问题时用

        public static object Loglocker = new object();
        public static void Loglock(string WriteLog, string fileName)//锁定写日志，主要是可能多线程同时调用
        {
            if (fileName.Equals("")) return;
            lock (Loglocker)
            {
                try
                {
                    Log(WriteLog, fileName);
                }
                catch
                {

                }
            }
        }
        #endregion

        public static void Write()
        {
            int count = 1;
            while (true)
            {
                count++;
                lock (Eventlocker)
                {
                    try
                    {
                        Log(count.ToString(), "Event");
                    }
                    catch
                    {

                    }
                }
                Thread.Sleep(1000);
            }
        }
    }
}
