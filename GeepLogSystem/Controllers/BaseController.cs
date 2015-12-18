using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GeepLogSystem.Controllers
{
    /// <summary>
    /// 基类控制器，其他mvc控制器都继承自此
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前控制器名称
        /// </summary>
        public string ActionName
        {
            get
            {
                return RouteData.Values["action"].ToString();
            }
        }

        /// <summary>
        /// Controller初始化
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Init();
        }

        /// <summary>
        /// BaseController初始化
        /// </summary>
        public void Init()
        {
            ViewBag.actionName = ActionName;
            ViewBag.PageSize = GeepLogSystem.Dao.Help.GetConfigValueForNull("PageSize");
        }
	}
}