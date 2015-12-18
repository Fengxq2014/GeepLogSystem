using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LogSystem
{
    public class WriteLog
    {
        private static volatile WriteLog instance = null;
        private static readonly object syncRoot = new object();
        public static WriteLog Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new WriteLog();
                        }
                    }
                }
                return instance;
            }
        }
        public void Log(LogRequestEntity entity)
        {
            PostWebRequest(JsonConvert.SerializeObject(entity), Encoding.UTF8);
        }

        public void Log(ErrorLogRequestEntity entity)
        {
            PostWebRequest(JsonConvert.SerializeObject(entity), Encoding.UTF8);
        }

        private void PostWebRequest(string paramData, Encoding dataEncode)
        {
            string postUrl = Help.GetConfigValueForNull("Log");
            if (string.IsNullOrWhiteSpace(postUrl))
            {
                throw new Exception("日志地址为空");
            }
            byte[] byteArray = dataEncode.GetBytes(paramData);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";

            webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();
            
        }
    }

      
}
