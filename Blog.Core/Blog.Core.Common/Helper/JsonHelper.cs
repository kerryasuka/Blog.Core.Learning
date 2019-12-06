using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Blog.Core.Common.Helper
{
    public class JsonHelper
    {
        /// <summary>
        /// 转换对象为Json格式数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>字符格式的Json数据</returns>
        public static string ToJson<T>(object obj)
        {
            string result = string.Empty;
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    result = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 转换List<T>的数据为Json格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToJson<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                foreach (T item in list)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        serializer.WriteObject(ms, item);
                        sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Json格式字符转换为T类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T FromJson<T>(string jsonStr)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonStr)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
