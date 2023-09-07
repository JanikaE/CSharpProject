using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace PENet
{
    public static class IOCPTool
    {
        #region 数据处理
        /// <summary>
        /// 字节流分割
        /// </summary>
        /// <param name="byteList"></param>
        /// <returns>一个数据段</returns>
        public static byte[] SplitLogicBytes(ref List<byte> byteList)
        {
            byte[] buff = null;
            // 数据头描述数据段的长度
            if (byteList.Count > 4)
            {
                byte[] data = byteList.ToArray();
                // 获取数据头的内容
                int len = BitConverter.ToInt32(data, 0);
                if (byteList.Count >= len + 4)
                {
                    buff = new byte[len];
                    Buffer.BlockCopy(data, 4, buff, 0, len);
                    byteList.RemoveRange(0, len + 4);
                }
            }
            return buff;
        }

        /// <summary>
        /// 给数据加上数据头
        /// </summary>
        public static byte[] PackLenInfo(byte[] data)
        {
            int len = data.Length;
            byte[] package = new byte[len + 4];
            byte[] header = BitConverter.GetBytes(len);
            header.CopyTo(package, 0);
            data.CopyTo(package, 4);
            return package;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T msg) where T : IOCPMsg
        {
            byte[] data = null;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                bf.Serialize(ms, msg);
                ms.Seek(0, SeekOrigin.Begin);
                data = ms.ToArray();
            }
            catch (SerializationException e)
            {
                Error("Fail To Serialize. Reason:{}", e.Message);
            }
            finally
            {
                ms.Close();
            }
            return data;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] bytes) where T : IOCPMsg
        {
            T msg = null;
            MemoryStream ms = new MemoryStream(bytes);
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                msg = (T)bf.Deserialize(ms);
            }
            catch (SerializationException e)
            {
                Error("Fail To Deserialize. Reason:{0} bytesLen:{1}", e.Message, bytes.Length);
            }
            finally
            {
                ms.Close();
            }
            return msg;
        }
        #endregion

        #region LOG日志
        public static Action<string> LogFunc;
        public static Action<string, LogColor> ColorLogFunc;
        public static Action<string> WarnFunc;
        public static Action<string> ErrorFunc;

        public static void Log(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (LogFunc != null)
            {
                LogFunc(msg);
            }
            else
            {
                ConsoleLog(msg, LogColor.None);
            }
        }

        public static void ColorLog(string msg, LogColor color, params object[] args)
        {
            msg = string.Format(msg, args);
            if (ColorLogFunc != null)
            {
                ColorLogFunc(msg, color);
            }
            else
            {
                ConsoleLog(msg, color);
            }
        }

        public static void Warn(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (WarnFunc != null)
            {
                WarnFunc(msg);
            }
            else
            {
                ConsoleLog(msg, LogColor.Yellow);
            }
        }

        public static void Error(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (ErrorFunc != null)
            {
                ErrorFunc(msg);
            }
            else
            {
                ConsoleLog(msg, LogColor.Red);
            }
        }

        private static void ConsoleLog(string msg, LogColor color)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;
            msg = string.Format("Thread{0}:{1}", threadID, msg);
            switch (color)
            {
                case LogColor.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogColor.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogColor.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case LogColor.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        #endregion
    }

    /// <summary>
    /// 网络数据协议消息体
    /// </summary>
    [Serializable]
    public abstract class IOCPMsg
    {
    }

    public enum LogColor
    {
        None,
        Red,
        Green,
        Blue,
        Yellow
    }
}
