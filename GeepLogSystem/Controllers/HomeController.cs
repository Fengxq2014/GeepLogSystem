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
            ViewBag.PadNormal = SearchService.LogCount(LogNameEnum.ZFT, false);
            ViewBag.PadError = SearchService.LogCount(LogNameEnum.ZFT, true);
            //ViewBag.Cvept = SearchService.LogCount(LogNameEnum.CVEPT, false);
            //ViewBag.CveptError = SearchService.LogCount(LogNameEnum.CVEPT, true);
            //ViewBag.Management = SearchService.LogCount(LogNameEnum.Management, false);
            //ViewBag.ManagementError = SearchService.LogCount(LogNameEnum.Management, true);
            //ViewBag.ProtocolComponent = SearchService.LogCount(LogNameEnum.ProtocolComponent, false);
            //ViewBag.ProtocolComponentError = SearchService.LogCount(LogNameEnum.ProtocolComponent, true);
            return View("NewIndex");
        }

        /// <summary>
        /// pad智付通
        /// </summary>
        /// <param name="p">页数</param>
        /// <returns></returns>
        public ActionResult ZFT(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "pad智付通";
            ViewBag.ActiveId = 2;
            return View("Common");
        }

        /// <summary>
        /// 转账电话
        /// </summary>
        /// <param name="p">页数</param>
        /// <returns></returns>
        public ActionResult CVEPT(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "转账电话";
            ViewBag.ActiveId = 3;
            return View("Common");
        }

        /// <summary>
        /// 管理端
        /// </summary>
        /// <param name="p">页数</param>
        /// <returns></returns>
        public ActionResult Management(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "管理端";
            ViewBag.ActiveId = 4;
            return View("Common");
        }

        /// <summary>
        /// 协议组件
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult ProtocolComponent(int p = 1)
        {
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = ViewBag.ActionName };
            ViewBag.ZFTList = SearchService.Search(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "协议组件";
            ViewBag.ActiveId = 5;
            return View("Common");
        }

        /// <summary>
        /// pad智付通（异常）
        /// </summary>
        /// <returns>视图</returns>
        public ActionResult ZFTError(int p = 1)
        {
            var model = new MongoDBHelper<GeepLogSystem.Models.ZFTLogModel>();
            long count;
            ViewBag.ZFTList = model.FindAllError(LogNameEnum.ZFT, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "pad智付通（异常）";
            ViewBag.ActiveId = 6;
            return View("Common");
        }

        /// <summary>
        /// 转账电话（异常）
        /// </summary>
        /// <param name="p">页码</param>
        /// <returns></returns>
        public ActionResult CVEPTError(int p = 1)
        {
            var model = new MongoDBHelper<GeepLogSystem.Models.ZFTLogModel>();
            long count;
            ViewBag.ZFTList = model.FindAllError(LogNameEnum.CVEPT, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "转账电话（异常）";
            ViewBag.ActiveId = 7;
            return View("Common");
        }

        /// <summary>
        /// 管理端（异常）
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult ManagementError(int p = 1)
        {
            var model = new MongoDBHelper<GeepLogSystem.Models.ZFTLogModel>();
            long count;
            ViewBag.ZFTList = model.FindAllError(LogNameEnum.Management, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "管理端（异常）";
            ViewBag.ActiveId = 8;
            return View("Common");
        }

        /// <summary>
        /// 协议组件（异常）
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult ProtocolComponentError(int p = 1)
        {
            var model = new MongoDBHelper<GeepLogSystem.Models.ZFTLogModel>();
            long count;
            ViewBag.ZFTList = model.FindAllError(LogNameEnum.ProtocolComponent, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "协议组件（异常）";
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
            
            return View("NewSearch");
        }

        /// <summary>
        /// 总体统计
        /// </summary>
        /// <returns></returns>
        public ActionResult OverallStatistics()
        {
            ViewBag.Pad = SearchService.GetErrorLogCountOfWeek(LogNameEnum.ZFT);
            ViewBag.Cvept = SearchService.GetErrorLogCountOfWeek(LogNameEnum.CVEPT);
            ViewBag.Management = SearchService.GetErrorLogCountOfWeek(LogNameEnum.Management);
            ViewBag.ProtocolComponent = SearchService.GetErrorLogCountOfWeek(LogNameEnum.ProtocolComponent);
            return View();
        }

        /// <summary>
        /// pad智付通统计
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
        public string InsertLog(ZFTLogModel model)
        {
            byte[] by = new byte[Request.InputStream.Length];
            Request.InputStream.Read(by, 0, by.Length);
            string a = System.Text.Encoding.UTF8.GetString(by);
            ZFTLogModel insert = Newtonsoft.Json.JsonConvert.DeserializeObject<ZFTLogModel>(a);
            var Dao = new MongoDBHelper<GeepLogSystem.Models.ZFTLogModel>();
            var result = Dao.InsterOneAsync(insert);
            return null;
        }

        private string RegexMatch(string input, string querykey, System.Text.RegularExpressions.RegexOptions options)
        {
            return System.Text.RegularExpressions.Regex.Match(input, string.Format(@"(?<=(\&|\?|^)({0})\=).*?(?=\&|$)", querykey), options).Value.Trim();
        }

        public string inserttest(int num)
        {
            var Dao = new MongoDBHelper<GeepLogSystem.Models.ZFTLogModel>();
            
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 1; i <= num; i++)
            {
                var model = new ZFTLogModel();
                model.IsErrorLog = false;
                model.LogName = LogNameEnum.ZFT;
                model.AccNo = i.ToString();
                model.Time = DateTime.Now;
                var result = Dao.InsterOneAsync(model);
            }
            watch.Stop();
            return watch.Elapsed.ToStr();
        }

        public ActionResult jh(int p =1)
        {
            
            long count;
            SearchTermsModel search = new SearchTermsModel() { Action = "newindex" };
            ViewBag.ZFTList = SearchService.Search<log_list>(search, out count, p);
            ViewBag.Count = count;
            ViewBag.Page = p < 1 ? 1 : p;
            ViewBag.Title = "转账电话";
            ViewBag.ActiveId = 13;
            return View();
        }
    }
}