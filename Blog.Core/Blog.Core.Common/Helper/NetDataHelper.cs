using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Blog.Core.Common.Helper
{
    public class NetDataHelper
    {
        public static string Get(string serviceAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string res = streamReader.ReadToEnd();

            streamReader.Close();
            responseStream.Close();

            return res;
        }

        public static string Post(string serviceAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "POST";
            request.ContentType = "application/json";
            string content = @"{ ""mmmm"": ""89e"", ""nnnnnn"": ""0101943"", ""kkkkkkk"": ""e8sodijf9"" }";

            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write(content);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding != null || encoding.Length < 1)
            {
                encoding = "UTF-8";
            }

            string res = string.Empty;
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding)))
            {
                res = sr.ReadToEnd();
            }

            return res;
        }
    }
}
