using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeepLogSystem.Models
{
    /// <summary>
    /// 日志分类
    /// </summary>
    public class LogNameEnum
    {
        /// <summary>
        /// pad智付通
        /// </summary>
        public static string ZFT = "ZFTPAD";

        /// <summary>
        /// 转账电话
        /// </summary>
        public static string CVEPT = "CVEPT";

        /// <summary>
        /// 管理端
        /// </summary>
        public static string Management = "MANAGEMENT";

        /// <summary>
        /// 安卓
        /// </summary>
        public static string Android = "ANDROID";

        /// <summary>
        /// 脚本
        /// </summary>
        public static string Javascript = "JAVASCRIPT";

        /// <summary>
        /// 协议组件
        /// </summary>
        public static string ProtocolComponent = "PROTOCOLCOMPONENT";
    }
}