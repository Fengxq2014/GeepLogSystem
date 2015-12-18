using GeepLogSystem.Dao;
using GeepLogSystem.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace GeepLogSystem.Service
{
    /// <summary>
    /// 搜索服务类
    /// </summary>
    public static class SearchService
    {
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="searchTerms">搜索条件模型</param>
        /// <param name="count">总条数</param>
        /// <param name="p">页码</param>
        /// <returns></returns>
        public static IEnumerable<ZFTLogModel> Search(SearchTermsModel searchTerms, out long count, int p = 1)
        {
            if (searchTerms == null)
            {
                count = 0;
                return new List<ZFTLogModel>() { new ZFTLogModel() { AccNo = "筛选条件为空" } };
            }
            if (string.IsNullOrWhiteSpace(searchTerms.Action))
            {
                count = 0;
                return new List<ZFTLogModel>() { new ZFTLogModel() { AccNo = "action为空" } };
            }
            var model = new MongoDBHelper<ZFTLogModel>();
            var builder = Builders<ZFTLogModel>.Filter;
            FilterDefinition<ZFTLogModel> filter = builder.Exists("_id");

            if (searchTerms.AccNo)
            {
                //filter = builder.Regex("AccNo", new BsonRegularExpression("*" + searchTerms.q));
                filter = filter & builder.Regex("AccNo", searchTerms.q);
            }
            if (searchTerms.Action.ToLower() != "index" && searchTerms.Action.ToLower() != "search")
            {
                string actionName = searchTerms.Action.ToStr().ToLower();
                if (actionName == "zft" || actionName == "zfterror")
                {
                    filter = filter & builder.Eq("LogName", LogNameEnum.ZFT);
                }
                if (actionName == "cvept" || actionName == "cvepterror")
                {
                    filter = filter & builder.Eq("LogName", LogNameEnum.CVEPT);
                }
                if (actionName == "management" || actionName == "managementerror")
                {
                    filter = filter & builder.Eq("LogName", LogNameEnum.Management);
                }
                if (actionName == "protocolcomponent" || actionName == "protocolcomponenterror")
                {
                    filter = filter & builder.Eq("LogName", LogNameEnum.ProtocolComponent);
                }
            }
            if (searchTerms.ClassName)
            {
                filter = filter & builder.Eq("Class", searchTerms.q);
            }
            if (searchTerms.DevNo)
            {
                filter = filter & builder.Eq("DevNo", searchTerms.q);
            }
            if (searchTerms.Label)
            {
                filter = filter & builder.Eq("Label", searchTerms.q);
            }
            if (searchTerms.Description)
            {
                filter = filter & builder.Regex("Description", searchTerms.q);
            }
            if (searchTerms.Normal)
            {
                filter = filter & builder.Eq("IsErrorLog", false);
            }
            if (searchTerms.Error)
            {
                filter = filter & builder.Eq("IsErrorLog", true);
            }
            if (searchTerms.StartTime.Year >= 2015 && searchTerms.EndTime.Year >= 2015 && searchTerms.EndTime > searchTerms.StartTime)
            {
                filter = filter & builder.Gte("Time", searchTerms.StartTime) & builder.Lte("Time", searchTerms.EndTime);
            }
            if (!(searchTerms.AccNo || searchTerms.ClassName || searchTerms.Description || searchTerms.DevNo || searchTerms.Label) && !string.IsNullOrWhiteSpace(searchTerms.q))//什么都不选直接搜索
            {
                filter = builder.Regex("AccNo", searchTerms.q) | builder.Regex("Class", searchTerms.q) | builder.Regex("Description", searchTerms.q) | builder.Regex("DevNo", searchTerms.q) | builder.Regex("Label", searchTerms.q);
            }
            return model.FindAll(out count, filter, p);
        }

        /// <summary>
        /// 查询日志数量
        /// </summary>
        /// <param name="name">日志类型名称（参考LogNameEnum）</param>
        /// <param name="errorlog">是否是错误日志</param>
        /// <returns></returns>
        public static int LogCount(string name, bool errorlog)
        {
            var model = new MongoDBHelper<ZFTLogModel>();
            var builder = Builders<ZFTLogModel>.Filter;
            FilterDefinition<ZFTLogModel> filter = builder.Eq("LogName", name);
            if (errorlog)
            {
                filter = filter & builder.Eq("IsErrorLog", true);
            }
            else
            {
                filter = filter & builder.Eq("IsErrorLog", false);
            }
            long count;
            model.FindAll(out count, filter);
            return Convert.ToInt32(count);
        }

        /// <summary>
        /// 统计错误最多的类
        /// </summary>
        /// <param name="names">日志类型名</param>
        /// <param name="valuse">数量数组</param>
        /// <param name="name">类名数组</param>
        public static void Statistic(out string names, out string valuse, string name = null)
        {
            var model = new MongoDBHelper<ZFTLogModel>();
            var map = model.MapReduce().Take(10);
            List<Statistics> maps = new List<Statistics>();
            foreach (var c in map)
            {
                Statistics st = new Statistics
                {
                    Name = c.ElementAt(0).Value.ToString(),
                    Value = Convert.ToInt32(c.ElementAt(1).Value.ToString())
                };
                maps.Add(st);
            }
            List<string> n = new List<string>();
            List<int> v = new List<int>();
            maps.ForEach(x => n.Add(x.Name));
            maps.ForEach(x => v.Add(x.Value));
            names = Newtonsoft.Json.JsonConvert.SerializeObject(n);
            valuse = Newtonsoft.Json.JsonConvert.SerializeObject(v);
        }

        /// <summary>
        /// 求对应日志名称的周错误日志量，格式为：[1,3,4,5]
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        public static string GetErrorLogCountOfWeek(string logName)
        {
            var model = new MongoDBHelper<ZFTLogModel>();
            var builder = Builders<ZFTLogModel>.Filter;

            List<int> resultList = new List<int>();
            for (int i = 1; i < 8; i++)
            {
                FilterDefinition<ZFTLogModel> filter = builder.Exists("_id") & builder.Eq("LogName", logName) & builder.Eq("IsErrorLog", true);
                DateTime day = DateTime.Now.Date;
                filter = filter & builder.Gte("Time", day.AddDays(-i + 1)) & builder.Lte("Time", day.AddDays(-i + 2));
                long count;
                model.FindAll(out count, filter);
                resultList.Add(Convert.ToInt32(count));
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(resultList);
        }

        /// <summary>
        /// 扩展string.ToString()方法，为null时返回string.Empty
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static string ToStr(this object ob)
        {
            if (ob == null)
            {
                return string.Empty;
            }
            return ob.ToString().Trim();
        }
    }
}