using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeepLogSystem.Dao;
using GeepLogSystem.Models;
using GeepLogSystem.Service;

namespace GeepLogSystem.Controllers
{
    /// <summary>
    /// 主控制器
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Wxback = SearchService.LogCount(LogNameEnum.Wxback, false);
            ViewBag.WxbackError = SearchService.LogCount(LogNameEnum.Wxback, true);
            ViewBag.Front = SearchService.LogCount(LogNameEnum.Front, false);
            ViewBag.FrontError = SearchService.LogCount(LogNameEnum.Front, true);
            ViewBag.Wxabc = SearchService.LogCount(LogNameEnum.WxABC, false);
            ViewBag.WxabctError = SearchService.LogCount(LogNameEnum.WxABC, true);
            //ViewBag.Management = SearchService.LogCount(LogNameEnum.Management, false);
            //ViewBag.ManagementError = SearchService.LogCount(LogNameEnum.Management, true);
            //ViewBag.ProtocolComponent = SearchService.LogCount(LogNameEnum.ProtocolComponent, false);
            //ViewBag.ProtocolComponentError = SearchService.LogCount(LogNameEnum.ProtocolComponent, true);
            return View("Index");
        }

        /// <summary>
        /// 微信后端
        /// </summary>
        /// <param name="p">页数</param>
        /// <returns></returns>
        public ActionResult Wxback(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "微信后端";
            ViewBag.ActiveId = 2;
            return View("Common");
        }

        /// <summary>
        /// 微信后端异常
        /// </summary>
        /// <param name="p">页数</param>
        /// <returns></returns>
        public ActionResult WxbackError(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName,Error = true};
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "微信后端异常";
            ViewBag.ActiveId = 6;
            return View("Common");
        }

        /// <summary>
        /// 前端
        /// </summary>
        /// <param name="p">页数</param>
        /// <returns></returns>
        public ActionResult Front(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "前端";
            ViewBag.ActiveId = 3;
            return View("Common");
        }

        /// <summary>
        /// 前端
        /// </summary>
        /// <param name="p">页数</param>
        /// <returns></returns>
        public ActionResult FrontError(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName,Error = true };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "前端异常";
            ViewBag.ActiveId = 7;
            return View("Common");
        }

        /// <summary>
        /// 前端
        /// </summary>
        /// <param name="p">页数</param>
        /// <returns></returns>
        public ActionResult WxABC(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "特色交易";
            ViewBag.ActiveId = 4;
            return View("Common");
        }

        /// <summary>
        /// 前端
        /// </summary>
        /// <param name="p">页数</param>
        /// <returns></returns>
        public ActionResult WxABCError(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName, Error = true };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "特色交易异常";
            ViewBag.ActiveId = 8;
            return View("Common");
        }

        public ActionResult WxBI(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "微信批量导入";
            ViewBag.ActiveId = 5;
            return View("Common");
        }

        public ActionResult WxBIError(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName, Error = true };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "微信批量导入异常";
            ViewBag.ActiveId = 9;
            return View("Common");
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns>视图</returns>
        public ActionResult Search(SearchTermsModel search, int p = 1, string at = null)
        {
            //long count;
            //if (!string.IsNullOrWhiteSpace(at))
            //{
            //    search.Action = at;
            //}
            //ViewBag.ZFTList = SearchService.Search(search, out count, p);
            //ViewBag.Count = count;
            //ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.ActiveId = 10;
            return View("NewSearch");
        }

        /// <summary>
        /// 总体统计
        /// </summary>
        /// <returns></returns>
        public ActionResult OverallStatistics()
        {
            ViewBag.Wxback = SearchService.GetErrorLogCountOfWeek(LogNameEnum.Wxback);
            ViewBag.Front = SearchService.GetErrorLogCountOfWeek(LogNameEnum.Front);
            //ViewBag.Management = SearchService.GetErrorLogCountOfWeek(LogNameEnum.Management);
            //ViewBag.ProtocolComponent = SearchService.GetErrorLogCountOfWeek(LogNameEnum.ProtocolComponent);
            return View();
        }

        /// <summary>
        /// 统计
        /// </summary>
        /// <returns></returns>
        public ActionResult ZFTStatistics()
        {
            string names = string.Empty;
            string values = string.Empty;
            SearchService.Statistic(out names, out values);
            ViewBag.Names = names;
            ViewBag.Values = values;
            return View();
        }

        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="model">日志模型</param>
        /// <returns></returns>
        public string InsertLog(log model)
        {
            byte[] by = new byte[Request.InputStream.Length];
            Request.InputStream.Read(by, 0, by.Length);
            string a = System.Text.Encoding.UTF8.GetString(by);
            log insert = Newtonsoft.Json.JsonConvert.DeserializeObject<log>(a);
            var Dao = new MongoDBHelper<GeepLogSystem.Models.log>();
            var result = Dao.InsterOneAsync(insert);
            return null;
        }

        private string RegexMatch(string input, string querykey, System.Text.RegularExpressions.RegexOptions options)
        {
            return System.Text.RegularExpressions.Regex.Match(input, string.Format(@"(?<=(\&|\?|^)({0})\=).*?(?=\&|$)", querykey), options).Value.Trim();
        }

        public string inserttest(int num)
        {
            var Dao = new MongoDBHelper<GeepLogSystem.Models.log>();
            
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 1; i <= num; i++)
            {
                var model = new log();
                model.IsErrorLog = false;
                model.LogName = LogNameEnum.ZFT;
                model.AccNo = i.ToString();
                model.Time = DateTime.Now;
                var result = Dao.InsterOneAsync(model);
            }
            watch.Stop();
            return watch.Elapsed.ToStr();
        }

    }
}