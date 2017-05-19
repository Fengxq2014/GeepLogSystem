using GeepLogSystem.Dao;
using GeepLogSystem.Models;
using GeepLogSystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        public string PostInsert(GeepLogSystem.Models.log model)
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
            var Dao = new MongoDBHelper<GeepLogSystem.Models.log>();
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

        public IEnumerable<log_list> GetLog([FromUri]SearchTermsModel search,int p = 1, int pagesize = 3)
        {
            long count;
            search.Action = "newindex";
            return SearchService.Search<log_list>(search, out count, p, pagesize);
            //return SearchService.Search(search, out count, p, pagesize);
        }

        [HttpGet]
        public IEnumerable<log> TextSearch([FromUri]string q, int p = 1)
        {
            string qs = string.Join("\" \"", q.Split(' '));
            qs = qs.Insert(0, "\""); 
            qs = qs.Insert(qs.Length, "\"");
            var lists = new MongoDBHelper<log>().command(qs);
            //var list = lists.GetEnumerator();
            //for (int i = 0; i < lists.Count(); i++)
            //{
            //    list.MoveNext();
            //    list.Current.Time = SearchService.ConvertIntDateTime(double.Parse(list.Current.Time)).ToString("yyyy-MM-dd HH:mm:ss");
            //}
            
            return lists;
        }

        [HttpGet]
        public string Export(string q)
        {
            string timeName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var rootPath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath).ToLower();
            new MongoDBHelper<log_list>().Export(rootPath + "/" + timeName + ".txt", q);
            return rootPath + "/" + timeName + ".txt";
        }
    }
}
