using GeepLogSystem.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeepLogSystem.Controllers
{
    /// <summary>
    /// 插入日志
    /// </summary>
    public class InsertLogController : ApiController
    {
        /// <summary>
        /// 插入单条日志
        /// </summary>
        /// <param name="model">ZFTLogModel模型类</param>
        public string PostInsert(GeepLogSystem.Models.ZFTLogModel model)
        {
            if (model == null)
            {
                return "null";
            }
            //byte[] by = new byte[Request.InputStream.Length];
            //Request.InputStream.Read(by, 0, by.Length);
            //string a = System.Text.Encoding.Default.GetString(by);
            //byte[] by = Request.Content.ReadAsByteArrayAsync().Result;
            //string a = Request.Content.ReadAsStringAsync().Result;
            byte[] byts = new byte[System.Web.HttpContext.Current.Request.InputStream.Length];
            System.Web.HttpContext.Current.Request.InputStream.Read(byts, 0, byts.Length);
            string req = System.Text.Encoding.Default.GetString(byts);
            model.Ip = GetIp();
            model.Time = DateTime.Now;
            var Dao = new MongoDBHelper<GeepLogSystem.Models.ZFTLogModel>();
            //var result = Dao.InsterOneAsync(model);
            return "ok";
        }

         //<summary>
         //获取ip
         //</summary>
         //<returns>ip</returns>
        private string GetIp()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // 有代理
            {
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // 返回真实ip
            }
            else// 没有代理
            {
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //没有获取到真实ip则返回代理ip
            } 
        }
    }
}
